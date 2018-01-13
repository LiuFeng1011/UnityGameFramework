using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class UserDataMode  {
	public long userid = 1000;// 角色ID
	public string username = "null";// 角色名称
	public string itemList = "";//道具列表
	public int level = 1;
	public int viplevel = 1;

	public void SetData(UserData data){
		userid = data.userid;// 角色ID
		username = data.username;// 角色名称
		itemList = data.itemList;//道具列表
		level = data.level;
		viplevel = data.viplevel;
	}
}
