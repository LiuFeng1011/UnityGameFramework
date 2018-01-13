using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatRequest : Request {
	int chattype;// 聊天类型

	long targetid;//目标id 嘲讽时使用
	int param;//发送表情时为 表情id，嘲讽时0表示嘲讽1表示点赞
	string msg;//消息

	public override int GetProtocol(){
		return NetProtocols.CHAT;
	}

	/// <summary>
	/// 普通聊天
	/// </summary>
	/// <param name="msg">聊天内容.</param>
	public void ChatNormal(string msg){
		chattype = NetProtocols.CHAT_NORMAL;
		this.msg = msg;
	}

	/// <summary>
	/// 发送表情
	/// </summary>
	/// <param name="faceid">表情id.</param>
	public void ChatFace(int faceid){
		chattype = NetProtocols.CHAT_FACE;
		this.param = faceid;
	}

	/// <summary>
	/// 嘲讽
	/// </summary>
	/// <param name="targetid">目标玩家uid.</param>
	/// <param name="param">0赞 1嘲讽.</param>
	public void ChatTaunt(int targetid,int param){
		chattype = NetProtocols.CHAT_TAUNT;
		this.param = param;
		this.targetid = targetid;

	}


	public override void Serialize(DataStream writer)
	{
		base.Serialize(writer);

		writer.WriteSInt64(chattype);

		switch(chattype){
		case NetProtocols.CHAT_NORMAL:
			writer.WriteString16(msg);
			break;
		case NetProtocols.CHAT_FACE:
			writer.WriteSInt32(param);
			break;
		case NetProtocols.CHAT_TAUNT:
			writer.WriteSInt64(targetid);
			writer.WriteSInt32(param);
			break;
		}

	}

}
