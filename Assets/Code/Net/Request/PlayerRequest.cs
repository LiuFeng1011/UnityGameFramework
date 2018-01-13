using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRequest : Request {

	int type;
	int itemid;
	int itemcount;
	string nickname;
	string headid;

	int colorid;
	int roleid;

	int taskid;

	public override int GetProtocol(){
		return NetProtocols.PLAYER;
	}

	public void GetPlayerData(){
		type = NetProtocols.PLAYER_DATA;
	}

	public void GetItemCount(int itemid){
		type = NetProtocols.PLAYER_ITEMCOUNT;
		this.itemid = itemid;
	}

	public void ChangeName(string name){
		type = NetProtocols.PLAYER_CHANGENAME;
		nickname = name;
	}

	public void ChangeHead(string headid){
		type = NetProtocols.PLAYER_CHANGEHEAD;
		this.headid = headid;
	}
	public void BuyItem(int id){
		type = NetProtocols.PLAYER_BUY_ITEM;
		itemid = id;
	}

	public void UseItem(int id,int count){
		type = NetProtocols.PLAYER_USE_ITEM;
		itemid = id;
		itemcount = count;
		
	}

	public void SetNameColor(int id){
		type = NetProtocols.PLAYER_SET_NAMECOLOR;
		colorid = id;
	}

	public void SetRoleId(int id){
		type = NetProtocols.PLAYER_SET_ROLE;
		roleid = id;
	}

	public void TaskFinished(int taskid){
		type = NetProtocols.PLAYER_FINISHED_TASK;
		this.taskid = taskid;
	}

	/// <summary>
	/// 购买商店道具
	/// </summary>
	/// <param name="goods_id">goods_id.</param>
	public void Shopping(int goods_id){
		type = NetProtocols.PLAYER_SHOPPING;
		this.itemid = goods_id;
	}

	public override void Serialize(DataStream writer)
	{
		base.Serialize(writer);
		writer.WriteSInt32(type);
		switch(type){
		case NetProtocols.PLAYER_DATA:
			break;
		case NetProtocols.PLAYER_ITEMCOUNT:
			writer.WriteSInt32(itemid);
			break;
		case NetProtocols.PLAYER_CHANGENAME:
			writer.WriteString16(nickname);
			break;
		case NetProtocols.PLAYER_CHANGEHEAD:
			writer.WriteString16(headid);
			break;
		case NetProtocols.PLAYER_BUY_ITEM:
			writer.WriteSInt32(itemid);
			break;
		case NetProtocols.PLAYER_USE_ITEM:
			writer.WriteSInt32(itemid);
			writer.WriteSInt32(itemcount);
			break;
		case NetProtocols.PLAYER_SET_NAMECOLOR:
			writer.WriteSInt32(colorid);
			break;
		case NetProtocols.PLAYER_SET_ROLE:
			writer.WriteSInt32(roleid);
			break;
		case NetProtocols.PLAYER_FINISHED_TASK:
			writer.WriteSInt32(taskid);
			break;
		case NetProtocols.PLAYER_SHOPPING:
			writer.WriteSInt32(itemid);
			break;
		}
	}

}
