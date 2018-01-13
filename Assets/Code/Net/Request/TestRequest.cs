using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRequest : Request {


	public override int GetProtocol(){
		return NetProtocols.TEST;
	}


	public override void Serialize(DataStream writer)
	{
		base.Serialize(writer);
	}
}
