using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageResp : Resp {

	string msg;

	public override int GetProtocol(){
		return NetProtocols.MESSAGE;
	}

	public override void Deserialize(DataStream reader)
	{
		base.Deserialize(reader);
		msg = reader.ReadString16();
	}

	public string GetMsg(){
		return msg;
	}
}
