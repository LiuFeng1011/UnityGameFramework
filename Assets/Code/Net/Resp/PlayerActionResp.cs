using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionResp : Resp{

	public int type;
	public long uid;
	//玩家坐标
	public Vector3 pos ;
	//玩家移动速度(单位向量)
	public Vector3 speep ;

	//朝向
	public float rotation;

	//目标点坐标
	public long targetid ;

	// 0 蹲下 1 站起
	public byte isdown;

	public override int GetProtocol(){
		return NetProtocols.PLAYER_ACTION;
	}

	public override void Deserialize(DataStream reader)
	{
		base.Deserialize(reader);
		type = reader.ReadSInt32();
		uid = reader.ReadSInt64();
		switch(type){
		case NetProtocols.PLAYER_ACTION_MOVE:
			pos = NetTools.VectorDeserialize(reader);
			speep = NetTools.VectorDeserialize(reader);
			break;
		case NetProtocols.PLAYER_ACTION_STOP:
			pos = NetTools.VectorDeserialize(reader);
			rotation = (float)reader.ReadSInt32() / NetTools.FLOAT_PRECISION;
			break;
		case NetProtocols.PLAYER_ACTION_JUMP:
			pos = NetTools.VectorDeserialize(reader);
			break;
		case NetProtocols.PLAYER_ACTION_ATK:
			targetid = reader.ReadSInt64();
			rotation = (float)reader.ReadSInt32() / NetTools.FLOAT_PRECISION;
			pos = NetTools.VectorDeserialize(reader);
			break;
		case NetProtocols.PLAYER_ACTION_ROTATE:
			rotation = (float)reader.ReadSInt32() / NetTools.FLOAT_PRECISION;
			isdown = reader.ReadByte();
			break;
		case NetProtocols.PLAYER_ACTION_DOWN:
			isdown = reader.ReadByte();
			break;
		case NetProtocols.PLAYER_ACTION_LOCK:
			pos = NetTools.VectorDeserialize(reader);
			rotation = (float)reader.ReadSInt32() / NetTools.FLOAT_PRECISION;
			isdown = reader.ReadByte();
			break;
		}
	}
	


}
