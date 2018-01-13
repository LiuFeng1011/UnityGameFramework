using UnityEngine;
using System.Collections;

public class ServerModuleTest {
	int a = 0;
	int b = 0;
	string c = "";
	public void serialize(DataStream reader) {
		a = reader.ReadSInt32();
		b = reader.ReadSInt32();
		c = reader.ReadString16();

		Log();
	}


	public void Log(){
		NetTools.Log("[ServerModuleTest] a : " + a + "  b : " + b + "  c : " + c);
	}
}
