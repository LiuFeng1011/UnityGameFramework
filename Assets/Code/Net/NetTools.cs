using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NetTools {
	//游戏浮点数精度
	public static float FLOAT_PRECISION = 10000f;

	//解析服务器坐标数据流
	public static Vector3 VectorDeserialize(DataStream reader){
		float x = (float)reader.ReadSInt32() / FLOAT_PRECISION;
		float y = (float)reader.ReadSInt32() / FLOAT_PRECISION;
		float z = (float)reader.ReadSInt32() / FLOAT_PRECISION;
		return new Vector3(x,y,z);
	}

	//序列化坐标数据流
	public static void VectorSerialize(DataStream writer,Vector3 v){
		writer.WriteSInt32((int)(v.x * FLOAT_PRECISION));
		writer.WriteSInt32((int)(v.y * FLOAT_PRECISION));
		writer.WriteSInt32((int)(v.z * FLOAT_PRECISION));
	}

	public static void Log(string msg){
		//if(GameConst.IS_SHOW_NET_LOG) 
			Debug.Log("[NetWork Log] " + msg);
	}

	public static void LogError(string msg){
		//if(GameConst.IS_SHOW_NET_LOG) 
			Debug.LogError("[NetWork LogError] " + msg);
	}
}
