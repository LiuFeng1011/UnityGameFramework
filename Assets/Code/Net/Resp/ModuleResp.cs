using UnityEngine;
using System.Collections;

public class ModuleResp : Resp {
	public override int GetProtocol(){
		return NetProtocols.RESP_MODULE;
	}

	public override void Deserialize(DataStream reader)
	{
		base.Deserialize(reader);

		int moduleCount = reader.ReadSInt32();
		for(int i = 0 ; i < moduleCount ; i ++){
			int moduleType = reader.ReadSInt32();

			NetTools.Log("moduleType : " + moduleType);
			switch(moduleType)
			{
			//在这里解析所有模块
				case GameModuleType.TEST:
				ServerModuleTest t = new ServerModuleTest();
				t.serialize(reader);
				break;
//			case GameModuleType.TEST:
//				ServerModuleTest t = new ServerModuleTest();
//				t.serialize(reader);
//				break;
//			case GameModuleType.TEST:
//				ServerModuleTest t = new ServerModuleTest();
//				t.serialize(reader);
//				break;
			}
		}

	}

}
