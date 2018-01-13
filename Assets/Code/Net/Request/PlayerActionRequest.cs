using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionRequest : Request {
	int type;
	//玩家坐标
	Vector3 pos;
	//玩家移动速度(单位向量)
	Vector3 speep;

	//朝向
	float rotation;

	//目标点坐标
	long targetid;

	byte isdown;

	public override int GetProtocol(){
		return NetProtocols.PLAYER_ACTION;
	}

	//移动
	public void Move(Vector3 pos,Vector3 speed){

        Debug.Log("移动：：：：：");
		type = NetProtocols.PLAYER_ACTION_MOVE;
		this.pos = pos;
		this.speep = speed;
	}
	//停止
	public void Stop(Vector3 pos,float rotation){
		type = NetProtocols.PLAYER_ACTION_STOP;
		this.pos = pos;
		this.rotation = rotation;
	}
	//跳跃
	public void Jump(Vector3 pos){
		type = NetProtocols.PLAYER_ACTION_JUMP;
		this.pos = pos;
	}
	//攻击
	public void Atk(long targetid,float rotation = 0){
		Atk(targetid,rotation,Vector3.zero);
	}
	public void Atk(long targetid,float rotation,Vector3 pos){
		type = NetProtocols.PLAYER_ACTION_ATK;
		this.targetid = targetid;
		this.rotation = rotation;
		this.pos = pos;
	}
	//旋转
	//isdown : 0 开始旋转 1停止旋转
	public void Rotate(float rotation,byte isdown = 0){
		type = NetProtocols.PLAYER_ACTION_ROTATE;
		this.rotation = rotation;
		this.isdown = isdown;
	}
	//蹲下或站起
	//isdown : 0 蹲下 1 站起
	public void Down(byte isdown){
		type = NetProtocols.PLAYER_ACTION_DOWN;
		this.isdown = isdown;
	}

	public void Lock(Vector3 pos,float rotation,byte isdown){
		type = NetProtocols.PLAYER_ACTION_LOCK;
		this.pos = pos;
		this.rotation = rotation;
		this.isdown = isdown;
	}

	public override void Serialize(DataStream writer)
	{
		base.Serialize(writer);
		writer.WriteSInt32(type);

		switch(type){
		case NetProtocols.PLAYER_ACTION_MOVE:
			NetTools.VectorSerialize(writer,pos);
			NetTools.VectorSerialize(writer,speep);
			break;
		case NetProtocols.PLAYER_ACTION_STOP:
			NetTools.VectorSerialize(writer,pos);
			writer.WriteSInt32((int)(rotation*NetTools.FLOAT_PRECISION));
			break;
		case NetProtocols.PLAYER_ACTION_JUMP:
			NetTools.VectorSerialize(writer,pos);
			break;
		case NetProtocols.PLAYER_ACTION_ATK:
			writer.WriteSInt64(targetid);
			writer.WriteSInt32((int)(rotation*NetTools.FLOAT_PRECISION));
			NetTools.VectorSerialize(writer,pos);
			break;
		case NetProtocols.PLAYER_ACTION_ROTATE:
			writer.WriteSInt32((int)(rotation*NetTools.FLOAT_PRECISION));
			writer.WriteByte(isdown);
			break;
		case NetProtocols.PLAYER_ACTION_DOWN:
			writer.WriteByte(isdown);
			break;
		case NetProtocols.PLAYER_ACTION_LOCK:
			NetTools.VectorSerialize(writer,pos);
			writer.WriteSInt32((int)(rotation*NetTools.FLOAT_PRECISION));
			writer.WriteByte(isdown);
			break;
		}
	}
}
