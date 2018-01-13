using UnityEngine;
using System.IO;

public enum enMSObjID{
	en_obj_id_null		=	0,
	en_obj_id_obj_1		= 	1001,
	en_obj_id_obj_2,
}

//阵营
public enum enMSCamp{
	en_camp_player,//玩家
	en_camp_enemy,//敌人
	en_camp_neutral,//中立
	en_camp_other,//其他
}

public enum enDataType{
	en_datatype_int,
	en_datatype_float,
	en_datatype_string,
	en_datatype_end = 1000,
}

//角色动作
public enum enObjectAnimationType{
	en_animationtype_wait,//待机
	en_animationtype_run,//跑
	en_animationtype_jump,//跳
	en_animationtype_atk1,//攻击
	en_animationtype_atk2,
	en_animationtype_atk3,
	en_animationtype_atk4,
	en_animationtype_atk5,
}

//刷怪点类型
public enum enEnemyPointType{
	en_enemypoint_type_null,
	en_enemypoint_type_dailytiem,//时间间隔
	en_enemypoint_type_event,//

}

//AI Type
public enum enAIType{
	en_aitype_wait,//木桩
	en_aitype_counterattack,//反击
	en_aitype_active,//主动
	en_aitype_patrol,//巡逻
}

public static class GameConst  {
	
	public const string userDataFileName = "userdata";
	public const string CONF_FILE_NAME = ".msconfig";

	
	public static string GetLevelDataFilePath(string filename){
		if (!Directory.Exists(Application.persistentDataPath + "/LevelData"))
		{
			Directory.CreateDirectory(Application.persistentDataPath + "/LevelData");//创建文件夹

		}
		return Application.persistentDataPath + "/LevelData/" + filename;
	}

	public static string GetExcelFilePath(string filename){
		return Application.dataPath+"/ExcelTools/xlsx/"+filename;
	}

	public static string GetConfigFilePath(string tablename){
		string src = "";

		if (Application.platform == RuntimePlatform.Android)
		{
			src = "jar:file://" + Application.dataPath + "!/assets/Config/" + tablename;
		}else if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			src = "file://" + Application.dataPath + "/Raw/Config/" + tablename;
		}else{
			src = "file://" + Application.streamingAssetsPath + "/Config/" + tablename;
		}
		return src;
	}
	public static string GetConfigPath(){
		return "Assets/StreamingAssets/Config/";
	}

	public static string SaveConfigFilePath(string tablename)
	{
		return Application.streamingAssetsPath + "/Config/" + tablename;
	}

	public static string GetPersistentDataPath(string filename){
		return Application.persistentDataPath + "/" + filename;  
	}

	public static string[] objectAnimationName = {"wait","run","jump","atk1","atk2","atk3","atk4","atk5"};
}
