using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
public class PlayerData{
	public long uid;
	public string uname = "";
	public int changenamecount = 0;//修改昵称次数
	public int lv = 1;
	public int exp = 0;
	public string head = "";
	public long createTime = 0;// 创建时间
	public long lastLoginTime = 0;// 角色上次登录时间
	public long lastOnlineTime = 0;//上次在线时间

	public int selrole = -1;//选择的角色
	public int namecolor = -1;//选择的昵称颜色

	public Dictionary<int, int> itemList = new Dictionary<int, int>();//道具列表 key:道具id value:道具数量
	public List<int> hideItemList = new List<int>();//已解锁的隐藏道具列表
	public List<int> roleList = new List<int>() ;//已解锁的角色列表

	public void Deserialize(DataStream reader)
	{
		uid = reader.ReadSInt64();
		uname = reader.ReadString16();
		changenamecount = reader.ReadSInt32();
		lv = reader.ReadSInt32();
		exp = reader.ReadSInt32();
		head = reader.ReadString16();
		createTime = reader.ReadSInt64();
		lastLoginTime = reader.ReadSInt64();
		lastOnlineTime = reader.ReadSInt64();

		selrole = reader.ReadSInt32();
		namecolor = reader.ReadSInt32();

		int count = reader.ReadSInt32();
		for(int i = 0 ; i < count ; i ++){
			int _id = reader.ReadSInt32();
			int _count = reader.ReadSInt32();
			itemList.Add(_id,_count);
			NetTools.Log("id : " + _id + "   count : " + _count);
		}

		count = reader.ReadSInt32();
		for(int i = 0 ; i < count ;i ++){
			int _id = reader.ReadSInt32();
			hideItemList.Add(_id);
			NetTools.Log( "hide item id : " + _id);
		}

		count = reader.ReadSInt32();
		for(int i = 0 ; i < count ;i ++){
			int _id = reader.ReadSInt32();
			roleList.Add(_id);
			NetTools.Log( "role item id : " + _id);
		}

		Log();
	}

	public void Log(){
		NetTools.Log("PlayerData : " + 
			uid + 
			"   uname:" + uname + 
			"   lv:" + lv + 
			"   exp:" + exp + 
			"   head:" + head + 
			"   createTime:" + createTime + 
			"   lastLoginTime:" + lastLoginTime + 
			"   lastOnlineTime:" + lastOnlineTime  + 
			"   selrole:" + selrole  + 
			"   namecolor:" + namecolor 
		);
	}
}

public class PlayerResp : Resp {
	public int type;

	public PlayerData pd;

	public int itemid;
	public int itemcount;

	public string nickname;
	public string head;

	public int exp;
	public int lv;

	public Dictionary<int, int> itemList = new Dictionary<int, int>();//道具列表

	public int colorid;
	public int roleid;

	/// <summary>
	/// 每日任务数据
	/// key:任务id
	/// value:任务完成进度
	/// -1的值为任务发放日期时间戳，单位为秒。
	/// </summary>
	public Dictionary<int, int> taskData = new Dictionary<int, int>();

	public override int GetProtocol(){
		return NetProtocols.PLAYER;
	}

	public override void Deserialize(DataStream reader)
	{
		base.Deserialize(reader);
		type = reader.ReadSInt32();
		NetTools.Log("===============PlayerResp : type " + type);
		switch(type){
		case NetProtocols.PLAYER_DATA:
			pd = new PlayerData();
			pd.Deserialize(reader);
			break;
		case NetProtocols.PLAYER_ITEMCOUNT:
			itemid = reader.ReadSInt32();
			itemcount = reader.ReadSInt32();
			break;
		case NetProtocols.PLAYER_CHANGENAME:
			nickname = reader.ReadString16();
			break;
		case NetProtocols.PLAYER_CHANGEHEAD:
			head = reader.ReadString16();
			break;
		case NetProtocols.PLAYER_LVUP:
			exp = reader.ReadSInt32();
			lv = reader.ReadSInt32();
			break;
		case NetProtocols.PLAYER_GET_ITEMLSIT:
			int getitemcount = reader.ReadSInt32();
			for(int i =0  ; i < getitemcount ; i ++){
				itemList.Add(reader.ReadSInt32(),reader.ReadSInt32());
			}
			break;
		case NetProtocols.PLAYER_BUY_ITEM:
			itemid = reader.ReadSInt32();
			break;
		case NetProtocols.PLAYER_SET_NAMECOLOR:
			colorid = reader.ReadSInt32();
			break;
		case NetProtocols.PLAYER_SET_ROLE:
			roleid = reader.ReadSInt32();
			break;
		case NetProtocols.PLAYER_DAILYTASK_DATA:
			int count = reader.ReadSInt32();

			for(int i = 0 ; i < count ; i ++){
				taskData.Add(reader.ReadSInt32(),reader.ReadSInt32());
			}

			break;
		case NetProtocols.PLAYER_SHOPPING:
			itemid = reader.ReadSInt32();
			break;
		}
	}

}
