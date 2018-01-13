using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using LitJson;

public class UserDataManager : BaseSingleton<UserDataManager> {

	private UserData userData = null;
	private Dictionary<string, AHInt> itemList = new Dictionary<string, AHInt>();//道具列表

	public void Start(){
        Debuger.Log("===========初始化用户数据系统=============");
		userData = new UserData();

		//从存档中加载数据
		LoadData();

		//初始化 使数据可用
        InitData();

        Debuger.Log("----------用户数据系统初始化完成-------------");
	}

	public void InitData(){
		string itemstr = userData.itemList;
		if(itemstr != null &&  itemstr != ""){
			itemList = JsonMapper.ToObject<Dictionary<string, AHInt>>(userData.itemList);
			Debuger.Log("InitData : userData.itemList : " + userData.itemList);
		}
	}


	//====================角色ID============================
	public long GetUserid(){
		return userData.userid;
	}

	public string GetUserName(){
		return userData.username;
	}

	public void SetUserName(string name){
		userData.username = name;
		SaveData();
	}

	public int GetUserLevel(){
		return userData.level;
	}

	public void SetUserLevel(int level){
		userData.level = level;
		SaveData();
	}

	public int GetUserVipLevel(){
		return userData.viplevel;
	}

	public void SetUserVipLevel(int viplevel){
		userData.viplevel = viplevel;
		SaveData();
	}

	//====================道具============================
	public void AddItem(List<GameItem> items){
		foreach(GameItem item in items){
			if(itemList.ContainsKey(item.itemid)){
				int oldcount = itemList[item.itemid];
				itemList.Remove(item.itemid);
				itemList.Add(item.itemid,oldcount + item.itemcount);
				continue;
			}
			itemList.Add(item.itemid,item.itemcount);
		}

		userData.itemList = JsonMapper.ToJson(itemList);
		SaveData();
	}

	public bool UseItem(List<GameItem> items){
		foreach(GameItem item in items){
			if(!itemList.ContainsKey(item.itemid)){
				return false;
			}
			if(itemList[item.itemid] < item.itemcount ){
				return false;
			}

		}
		
		foreach(GameItem item in items){
			int oldcount = itemList[item.itemid];
			itemList.Remove(item.itemid);
			itemList.Add(item.itemid , oldcount - item.itemcount);
		}

		userData.itemList = JsonMapper.ToJson(itemList);
		SaveData();
		return true;
	}

	public int GetItemCount(string itemid){
		if(!itemList.ContainsKey(itemid)){
			return 0;
		}
		return itemList[itemid];
	}



	//======================================================

	public void LoadData(){

		string filepath = GameConst.GetPersistentDataPath(GameConst.userDataFileName);
		byte[] gzipdata = GameCommon.ReadByteToFile(filepath);

		UserDataMode userdata;

		if( gzipdata == null){
			userdata = new UserDataMode();

			CopyData(userdata);
			SaveData();
		}else{
			byte[] data = GameCommon.UnGZip(gzipdata);
			userdata = (UserDataMode)GameCommon.DeserializeObject(data);

			CopyData(userdata);
		}

	}

	public void SaveData(){
		UserDataMode userdatamode = new UserDataMode();

		userdatamode.SetData(userData);

		byte[] data = GameCommon.SerializeObject(userdatamode);
		byte[] gzipData = GameCommon.CompressGZip(data);
		GameCommon.WriteByteToFile(gzipData,GameConst.GetPersistentDataPath(GameConst.userDataFileName));
	}

	void CopyData(UserDataMode userdatamode){
		userData.SetData(userdatamode);
	}

}
