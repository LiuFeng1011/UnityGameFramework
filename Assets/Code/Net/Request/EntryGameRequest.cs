using UnityEngine;
using System.Collections;

public class EntryGameRequest : Request {

	long uid;
	int channelid;
	string token;
	public override int GetProtocol(){
		return NetProtocols.ENTRY_GAME;
	}

	public EntryGameRequest(long userid,int channelid,string token){
		this.uid = userid;
		this.channelid = channelid;
		this.token = token;
	}

	public override void Serialize(DataStream writer)
	{
		base.Serialize(writer);
		writer.WriteSInt64(uid);
		writer.WriteSInt32(channelid);
		writer.WriteString16(token);
		writer.WriteSInt64(uid);
	}
}
