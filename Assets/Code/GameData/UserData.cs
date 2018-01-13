using UnityEngine;
using System.Collections;

public class UserData {
	public AHLong userid;// 角色ID
	public AHString username;// 角色名称
	public AHString itemList = "";//道具列表
	public AHInt level = 1;
	public AHInt viplevel = 1;

	public void SetData(UserDataMode data){
		userid = data.userid;// 角色ID
		username = data.username;// 角色名称
		itemList = data.itemList;//道具列表
		level = data.level;
		viplevel = data.viplevel;
	}
}
