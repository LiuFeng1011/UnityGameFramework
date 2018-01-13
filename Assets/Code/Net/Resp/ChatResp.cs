using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatResp : Resp {

	public int chattype{ get;private set;}// 聊天类型
	public long sourceid{ get;private set;}//发送玩家id
	public long targetid{ get;private set;}//目标id 嘲讽时使用
	public int param{ get;private set;}//发送表情时为 表情id，嘲讽时0表示嘲讽1表示点赞
	public string msg{ get;private set;}//消息

	public override int GetProtocol(){
		return NetProtocols.CHAT;
	}

	public override void Deserialize(DataStream reader)
	{
		base.Deserialize(reader);
		chattype = reader.ReadSInt32();
		sourceid = reader.ReadSInt64();
		switch(chattype){
		case NetProtocols.CHAT_NORMAL:
			msg = reader.ReadString16();
			break;
		case NetProtocols.CHAT_FACE:
			param = reader.ReadSInt32();
			break;
		case NetProtocols.CHAT_TAUNT:
			targetid = reader.ReadSInt64();
			param = reader.ReadSInt32();
			break;
		}
	}

}
