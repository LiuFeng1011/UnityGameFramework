using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartResp : Resp {

	long clienttime;
	long servertime;


	public override int GetProtocol(){
		return NetProtocols.HEART;
	}

	public override void Deserialize(DataStream reader)
	{
		base.Deserialize(reader);
		clienttime = reader.ReadSInt64();
		servertime = reader.ReadSInt64();
	}

	public long GetClientTime(){
		return clienttime;
	}

	public long GetServertime(){
		return servertime;
	}

}
