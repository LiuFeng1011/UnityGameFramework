using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class test : MonoBehaviour {
	const int height = 40;
	const int width = 150;

	double clienttime = 0;

	float sendHeratTime = 0;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
		clienttime += (Time.deltaTime * 1000f);
		sendHeratTime += Time.deltaTime;

		if(sendHeratTime > 3){
			sendHeratTime = 0;
			HeartRequest req = new HeartRequest();
			req.SetTime((long)clienttime);
			req.Send();
		}
	}
	void Awake()
	{
//		GlobalBase t_builder = transform.GetComponent<GlobalBase>();
//		t_builder.Init();

		ProtoManager.Instance.AddRespDelegate(NetProtocols.ENTRY_GAME,Response);
		ProtoManager.Instance.AddRespDelegate(NetProtocols.ROOM,RoomResponse);
		ProtoManager.Instance.AddRespDelegate(NetProtocols.PLAYER_ACTION,PlayerActionResponse);
		ProtoManager.Instance.AddRespDelegate(NetProtocols.HEART,HeartResponse);
		ProtoManager.Instance.AddRespDelegate(NetProtocols.MESSAGE,MessageResponse);
		ProtoManager.Instance.AddRespDelegate(NetProtocols.PLAYER,PlayerResponse);
		ProtoManager.Instance.AddRespDelegate(NetProtocols.TEST,PlayerResponse);
	}
	
	void OnDisable()
	{
		ProtoManager.Instance.DelRespDelegate(NetProtocols.ENTRY_GAME,Response);
		ProtoManager.Instance.DelRespDelegate(NetProtocols.ROOM,RoomResponse);
		ProtoManager.Instance.DelRespDelegate(NetProtocols.PLAYER_ACTION,PlayerActionResponse);
		ProtoManager.Instance.DelRespDelegate(NetProtocols.HEART,HeartResponse);
		ProtoManager.Instance.DelRespDelegate(NetProtocols.MESSAGE,MessageResponse);
		ProtoManager.Instance.DelRespDelegate(NetProtocols.PLAYER,PlayerResponse);
		ProtoManager.Instance.DelRespDelegate(NetProtocols.TEST,PlayerResponse);
	}

	int high;
	void OnGUI()  
	{
		high = 10;
		if(CreateBtn( "connect"))  
		{   
			SocketHelper.GetInstance().Connect(ConnectCallBack,null);
		}  

		if(CreateBtn(  "send"))  
		{   
			
		}  

		if(CreateBtn(  "login"))  
		{  
			LoginManager.Instance().httpFinishedDelegate = LoginSuccess;
			LoginManager.Instance().Login("liufeng1","123456");
		}  

		if(CreateBtn(  "reg"))  
		{  
			LoginManager.Instance().httpFinishedDelegate = LoginSuccess;
			LoginManager.Instance().Regist("liufeng1","123456");
		}  
		if(CreateBtn(  "shopping"))  
		{  
			PlayerRequest req = new PlayerRequest();
			req.Shopping(1001);
			req.Send();
		} 
		if(CreateBtn(  "CHANGENAME"))  
		{  
			PlayerRequest req = new PlayerRequest();
			req.ChangeName("打算");
			req.Send();
		} 
		if(CreateBtn(  "Set name color"))  
		{  
			PlayerRequest req = new PlayerRequest();
			req.SetNameColor(4001002);
			req.Send();
		}  
		if(CreateBtn(  "Set role"))  
		{  
			PlayerRequest req = new PlayerRequest();
			req.SetRoleId(2001034);
			req.Send();
		}  

		if(CreateBtn(  "Buy item"))  
		{  
			PlayerRequest req = new PlayerRequest();
			req.BuyItem(2002001);
			req.Send();
		}  
		if(CreateBtn(  "Use item"))  
		{  
			PlayerRequest req = new PlayerRequest();
			req.UseItem(4001002,1);
			req.Send();
		}  

//		if(CreateBtn(  "CreateRoom"))  
//		{  
//			RoomRequest req = new RoomRequest();
//			req.CreateRoom(0);
//			req.Send();
//		}  

		if(CreateBtn(  "InRoom"))  
		{  
			RoomRequest req = new RoomRequest();
			req.InRoom("5555");
			req.Send();
		}  
		if(CreateBtn(  "Move"))  
		{  
			PlayerActionRequest req = new PlayerActionRequest();
			req.Move(new Vector3(1231,324,123),new Vector3(1,1,1));
			req.Send();
		}  

		if(CreateBtn(  "TEST"))  
		{  
			TestRequest req = new TestRequest();
			req.Send();
		} 
	}

	public bool CreateBtn(string btnname){
		bool b = GUI.Button (new Rect (width, high,100,height),btnname);
		high += height+5;
		return b;
	}

	void PlayerResponse(Resp r){
		PlayerResp resp = (PlayerResp)r;

		NetTools.Log("PlayerResponse type:" + resp.type);
		switch(resp.type){
		case NetProtocols.PLAYER_DATA:
			break;
		case NetProtocols.PLAYER_ITEMCOUNT:
			NetTools.Log("CHANGE ITEM COUNT  id : " + resp.itemid + "   count : " + resp.itemcount );
			break;
		case NetProtocols.PLAYER_CHANGENAME:
			NetTools.Log("CHANGE NAME :" + resp.nickname);
			break;
		case NetProtocols.PLAYER_LVUP:

			NetTools.Log("EXP :" + resp.exp + "  LV : " + resp.lv);
			break;
		case NetProtocols.PLAYER_GET_ITEMLSIT:

			NetTools.Log("GET_ITEMLSIT :" + JsonMapper.ToJson(resp.itemList));
			break;
		case NetProtocols.PLAYER_BUY_ITEM:
			NetTools.Log("BUY_ITEM :" + resp.itemid);

			break;
		case NetProtocols.PLAYER_SET_NAMECOLOR:
			NetTools.Log("SET NAMECOLOR :" + resp.colorid);
			break;
		case NetProtocols.PLAYER_SET_ROLE:
			NetTools.Log("SET ROLE :" + resp.roleid);
			break;
		case NetProtocols.PLAYER_DAILYTASK_DATA:

			NetTools.Log("========PLAYER_DAILYTASK_DATA========== ");
			foreach (KeyValuePair<int, int> pair in resp.taskData)  
			{  
				NetTools.Log(pair.Key + " " + pair.Value);  
			}  
			NetTools.Log("-------------------------------------");
			break;
		}
	}

	void MessageResponse(Resp r){
		MessageResp resp = (MessageResp)r;
		NetTools.Log(resp.GetMsg());
	}

	void HeartResponse(Resp r){
		HeartResp resp = (HeartResp )r;

		//发送时的客户端时间
		//NetTools.Log("client time : " + resp.GetClientTime());
		//服务器时间
		//NetTools.Log("server time : " + resp.GetServertime());
		//先用当前客户端时间 - 当前客户端时间 = 网络延迟
		//NetTools.Log("delay time : " +( (resp.GetServertime() - resp.GetClientTime()) / 2));
		//服务器时间同步到客户端
		clienttime = (double)resp.GetServertime();
	}

	void PlayerActionResponse(Resp r){
		PlayerActionResp resp = (PlayerActionResp)r;
		NetTools.Log("PlayerActionResponse : " + resp.GetProtocol());
		NetTools.Log("PlayerActionResponse type: " + resp.GetType());

		NetTools.Log("PlayerActionResponse uid: " + resp.uid);
		if(resp.type == NetProtocols.PLAYER_ACTION_MOVE){
			NetTools.Log("PLAYER_ACTION_MOVE : " + resp.pos.x + "  y : " + resp.pos.y);
		}
	}

	void RoomResponse(Resp r){
		RoomResp resp = (RoomResp)r;
		NetTools.Log("RoomResponse : " + resp.GetProtocol());
		NetTools.Log("＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝RoomResponse type: " + resp.GetType());

		if(resp.GetType() == NetProtocols.ROOM_IN){
			resp.GetRoomDate().Log();
		}else if(resp.GetType() == NetProtocols.ROOM_SHOW_RAND_ITEM){

			InGameItem item = resp.GetInGameItem();
			item.Log();

		}

		if(resp.GetType() >= 10 && resp.GetType() <= 14 ){
			NetTools.Log("＝＝＝＝＝＝＝＝＝＝＝＝ROOM MODE : " + resp.GetType());
		}


	}
	void Response(Resp resp){
		NetTools.Log("resp : " + resp.GetProtocol());
	}
	public void LoginSuccess(){
		NetTools.Log("LoginSuccess udi : " + LoginManager.Uid);

		SocketHelper.GetInstance().Connect(ConnectCallBack,null);
	}

	public void ConnectCallBack(){
		NetTools.Log("ConnectCallBack");
		EntryGameRequest req = new EntryGameRequest(LoginManager.Uid,1,LoginManager.Token);
		req.Send();
	}

	public void TestCallBack(){
		
	}
}
