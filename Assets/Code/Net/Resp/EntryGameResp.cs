using UnityEngine;
using System.Collections;

public class EntryGameResp : Resp {

	public long serverTime = -1;
	public override int GetProtocol(){
		return NetProtocols.ENTRY_GAME;
	}

	public override void Deserialize(DataStream reader)
	{
		base.Deserialize(reader);
		serverTime = reader.ReadSInt64();
	}
}
