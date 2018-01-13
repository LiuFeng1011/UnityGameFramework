using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoomRequest : Request {
	int type;
	string roomid;
	int roomType;
	int selmode;
	int itemid;
	Vector3 position;
	List<long> uidlist;

	public override int GetProtocol(){
		return NetProtocols.ROOM;
	}

	[Obsolete("This InRoom function must use string param", false)]
	public void InRoom(int roomid){ 
		Debug.LogError("InRoom function be must use string param!");
	}

	public void InRoom(string roomid){
		type = NetProtocols.ROOM_IN;
		this.roomid = roomid;
	}

	public void LeaveRoom(){
		type = NetProtocols.ROOM_LEAVE;
	}

	public void CreateRoom(int roomtype){
		type = NetProtocols.ROOM_CREATE;
		this.roomType = roomtype;
	}

	public void SelMode(int modeid){
		type = NetProtocols.ROOM_SEL_MODE;
		this.selmode = modeid;
	}

	/// <summary>
	/// 获得场景中的道具
	/// </summary>
	/// <param name="id">Identifier.</param>
	public void PickUpItem(int id){
		type = NetProtocols.ROOM_GET_RAND_ITEM;
		itemid = id;
	}

	/// <summary>
	/// 使用拾取到的道具，必须是主动道具
	/// </summary>
	/// <param name="id">Identifier.</param>
	/// <param name="pos">Position.</param>
	public void UseItem(int id,Vector3 pos){
		type = NetProtocols.ROOM_USE_ITEM;
		itemid = id;
		position = pos;
	}

	/// <summary>
	/// 炸弹爆炸
	/// </summary>
	public void Bomb(Vector3 pos,List<long> targetlist){
		type = NetProtocols.ROOM_ITEM_BOMB;
		position = pos;
		uidlist = targetlist;
	}

	/// <summary>
	/// 重置玩家坐标，当玩家坐标出现异常，或变身后与其他道具重叠时使用此接口
	/// </summary>
	public void ResetPos(){
		type = NetProtocols.ROOM_RESET_POSITION;
	}

	public override void Serialize(DataStream writer)
	{
		base.Serialize(writer);
		writer.WriteSInt32(type);

		switch(type){
		case NetProtocols.ROOM_IN : 
			writer.WriteString16(roomid);
			break;
		case NetProtocols.ROOM_LEAVE : 
			break;
		case NetProtocols.ROOM_CREATE : 
			writer.WriteSInt32(roomType);
			break;
		case NetProtocols.ROOM_SEL_MODE:
			writer.WriteSInt32(selmode);
			break;
		case NetProtocols.ROOM_GET_RAND_ITEM:
			writer.WriteSInt32(itemid);
			break;
		case NetProtocols.ROOM_USE_ITEM:
			writer.WriteSInt32(itemid);
			NetTools.VectorSerialize(writer,position);
			break;
		case NetProtocols.ROOM_ITEM_BOMB:
			NetTools.VectorSerialize(writer,position);
			writer.WriteSInt32(uidlist.Count);
			for(int i = 0 ; i < uidlist.Count ; i ++){
				writer.WriteSInt64(uidlist[i]);
			}
			break;
		case NetProtocols.ROOM_RESET_POSITION:
			break;
		}
	}
}
