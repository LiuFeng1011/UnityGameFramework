using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartRequest : Request {
	long clienttime;

	public override int GetProtocol(){
		return NetProtocols.HEART;
	}

	public void SetTime(long time){
		clienttime = time;
	}

	public override void Serialize(DataStream writer)
	{
		base.Serialize(writer);
		writer.WriteSInt64(clienttime);
	}


}
