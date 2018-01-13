using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using LitJson;

public enum enInGamePlayerState{
	wait,//旁观
	hide,//躲藏
	find,//寻找
}

public enum enRoomState{
	wait,//等待
	ready,//准备
	hide,//躲藏模式
	find,//寻找模式
	over,//结束
	result,//结果
}

public class InRoomPlayerManager{

	public long uid;
	public string uname;
	public int namecolor;
	public int selectItem;//玩家选择的物体
	//状态
	public enInGamePlayerState state;

	//玩家坐标
	public Vector3 pos;
	//玩家移动速度(单位向量)
	public Vector3 speep;
	//朝向
	public int rotation;

	public int life;

	public byte islock;//是否锁定

	public void Deserialize(DataStream reader)
	{
		uid = reader.ReadSInt64();
		uname = reader.ReadString16();
		namecolor = reader.ReadSInt32();

		selectItem = reader.ReadSInt32();
		state = (enInGamePlayerState)reader.ReadByte();

		if(state == enInGamePlayerState.wait) return;
		pos = NetTools.VectorDeserialize(reader);
		life = reader.ReadSInt32();
		speep = NetTools.VectorDeserialize(reader);
		rotation = reader.ReadSInt32();
		islock = reader.ReadByte();

	}

	public void Log(){
		NetTools.Log(
			"uid : " + uid + 
			" uname : " + uname + 
			" selectItem : " + selectItem + 
			" state : " + state 
		);
	}
}

public class RoomData {
	int roomType;//房间类型
	string roomId;//房间id
	long createTime;//房间创建时间
	long createPlayerId;//房主id

	enRoomState state;//房间状态
	long startTime;//当前回合开始时间 

	public void Deserialize(DataStream reader)
	{
		this.state = (enRoomState)reader.ReadByte();
		this.startTime = reader.ReadSInt64();
		this.roomType = reader.ReadSInt32();
		this.roomId = reader.ReadString16();
		this.createTime = reader.ReadSInt64();
		this.createPlayerId = reader.ReadSInt64();
	}

	public void Log(){
		NetTools.Log("roomType : " + roomType + " roomId : " + roomId + " createTime : " + createTime + " createPlayerId : " + createPlayerId);
	}

	public int GetRoomType(){return roomType;}
	[Obsolete("Plase use new function [GetStringRoomId]", false)]
	public int GetRoomId(){
		Debug.LogError("Plase use new function [GetStringRoomId]");
		return 0;
	}
	public string GetStringRoomId(){return roomId;}
	public long GetCreateTime(){return createTime;}
	public long GetCreatePlayerId(){return createPlayerId;}
	public enRoomState GetState(){return state;}
	public long GetStartTime(){return startTime;}

}

public class InGameItem{
	public int id;
	public long showtime;//出现时间
	public Vector3 pos;
	public int confid;
	public long uid;//拾取玩家id -1为没有被拾取

	public static InGameItem Deserialize(DataStream reader)
	{
		InGameItem item = new InGameItem();
		item.id = reader.ReadSInt32();
		item.showtime = reader.ReadSInt64();
		item.pos = NetTools.VectorDeserialize(reader);
		item.confid = reader.ReadSInt32();
		item.uid = reader.ReadSInt64();
		return item;
	}

	public void Log(){

		NetTools.Log("===========InGameItem===========");
		NetTools.Log("id : " + id);
		NetTools.Log("showtime : " + showtime);
		NetTools.Log("confid : " + confid);
		NetTools.Log("uid : " + uid);
		NetTools.Log("--------------------------------");
	}
}

public class RoomResp : Resp {
	int type;

	long uid;
	string uname;
	RoomData rd ;
	public InRoomPlayerManager pm { get;  private set; }

	long time;

	int modeid;

	int levelid;

	List<InRoomPlayerManager> playerList = new List<InRoomPlayerManager>();

	//玩家死亡
	long sourceid;//攻击方id
	long targetid;//死亡玩家id


	//胜利方 0隐藏者 1寻找者
	byte wintype = 0;
	//奖励列表 偶数位为道具id  奇数位为数量
	List<int> rewardList = new List<int>();

	InGameItem item;
	int itemid;//使用的道具id

	public int life{get; private set; }//当前血量

	public override int GetProtocol(){
		return NetProtocols.ROOM;
	}

	//解析玩家列表
	public void DeserializePlayerList(DataStream reader){
		byte playerCount = reader.ReadByte();
		NetTools.Log("DeserializePlayerList player count : " + playerCount);
		for(int i = 0 ; i < playerCount ; i ++){
			InRoomPlayerManager pm = new InRoomPlayerManager();
			pm.Deserialize(reader);
			playerList.Add(pm);
		}

	}

	public override void Deserialize(DataStream reader)
	{
		base.Deserialize(reader);
		type = reader.ReadSInt32();
		NetTools.Log("RoomResp : " + type);

		switch(type){
		case NetProtocols.ROOM_IN:
			rd = new RoomData();
			rd.Deserialize(reader);
			DeserializePlayerList(reader);
			break;
		case NetProtocols.ROOM_LEAVE:
			
			break;
		case NetProtocols.ROOM_INPLAYER:
//			uid = reader.ReadSInt64();
//			uname = reader.ReadString16();

			pm = new InRoomPlayerManager();
			pm.Deserialize(reader);
			break;
		case NetProtocols.ROOM_OUTPLAYER:
			uid = reader.ReadSInt64();
			break;
		case NetProtocols.ROOM_SEL_MODE:
			uid = reader.ReadSInt64();
			modeid = reader.ReadSInt32();
			break;
		case NetProtocols.ROOM_READY:
			time = reader.ReadSInt64();
			levelid = reader.ReadSInt32();
			break;
		case NetProtocols.ROOM_HIDE:
			time = reader.ReadSInt64();
			DeserializePlayerList(reader);
			break;
		case NetProtocols.ROOM_FIND:
			time = reader.ReadSInt64();
			break;
		case NetProtocols.ROOM_OVER:
			time = reader.ReadSInt64();
			
			wintype = reader.ReadByte();
			int rewardcount = reader.ReadSInt32();

			for(int i = 0 ; i < rewardcount ; i++){
				rewardList.Add(reader.ReadSInt32());
			}
			break;
		case NetProtocols.ROOM_RESULT:
			time = reader.ReadSInt64();
			break;
		case NetProtocols.ROOM_WAIT:
			time = reader.ReadSInt64();
			break;
		case NetProtocols.ROOM_PLAYER_DIE:
			sourceid = reader.ReadSInt64();
			targetid = reader.ReadSInt64();
			break;
		case NetProtocols.ROOM_SHOW_RAND_ITEM:
			item = InGameItem.Deserialize(reader);
			break;
		case NetProtocols.ROOM_GET_RAND_ITEM:
			item = InGameItem.Deserialize(reader);
			break;
		case NetProtocols.ROOM_USE_ITEM:
			item = InGameItem.Deserialize(reader);
			targetid = reader.ReadSInt64();
			break;
		case NetProtocols.ROOM_PLAYER_LIFE:
			uid = reader.ReadSInt64();
			life = reader.ReadSInt32();
			break;
		}
	}

	public RoomData GetRoomDate(){
		return rd;
	}

	public long GetUid(){
		return uid;
	}
	public int GetType(){
		return type;
	}
	public List<InRoomPlayerManager> GetPlayerList(){
		return playerList;
	}

	public int GetModeid(){
		return modeid;
	}

	public string GetName(){
		return uname;
	}

	public int GetLevelId(){
		return levelid;
	}

	public long GetTime(){
		return time;
	}

	/// <summary>
	/// 玩家死亡,攻击方玩家id
	/// </summary>
	/// <returns>The source I.</returns>
	public long GetSourceID(){
		return sourceid;
	}
	/// <summary>
	/// 玩家死亡，死亡玩家id
	/// </summary>
	/// <returns>The target I.</returns>
	public long GetTargetID(){
		return targetid;
	}

	/// <summary>
	/// 胜利方 
	/// </summary>
	/// <returns>0隐藏者 1寻找者</returns>
	public byte GetWinType(){
		return wintype;
	}

	/// <summary>
	/// 奖励列表
	/// </summary>
	/// [1,2,3,4] 物品id [1]有 2个，物品id［3］有4个
	/// <returns>偶数位为物品id,奇数位为物品数量</returns>
	public List<int> GetRewardList(){
		return rewardList;
	}

	/// <summary>
	/// 获取关卡随机道具
	/// </summary>
	/// <returns>The in game item.</returns>
	public InGameItem GetInGameItem(){
		return item;
	}

	/// <summary>
	/// 道具id
	/// </summary>
	/// <returns>The item identifier.</returns>
	public int GetItemId(){
		return itemid;
	}

}
