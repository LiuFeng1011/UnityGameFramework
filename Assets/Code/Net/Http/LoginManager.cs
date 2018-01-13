using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using System.IO;
using LitJson;


public class LoginManager : MonoBehaviour{
	public const string USERNAME_FIELD = "username";
	public const string PASSWORD_FIELD = "password";
	//登录地址
	public const string HTTP_LOGIN_SERVER_ADDRESS = "http://user.dreamgear82.com/user/user.php";

	private static LoginManager instance ;

	public static long Uid = -1;
	public static string UName = "";
	public static string Token = "";

	void Awake()
	{
		instance = this;
	}
	public static LoginManager Instance(){
		return instance;
	}

	private enum LoginType
	{
		NETWORK_LOGIN = 0,
		NETWORK_REGIST = 1,
		NETWORK_QUICKLY = 2
	}
	public struct NetParam
	{
		public byte type;
		public string uri;
		public string usrname;
		public string password;
		public void SetParam(byte _type, string _uri, string _name, string _pass)
		{
			type = _type;
			uri = _uri;
			usrname = _name;
			password = _pass;
		}
	}

	static List<NetParam> readerResponse = new List<NetParam>();

	public delegate void HttpFinishedDelegate();

	public HttpFinishedDelegate httpFinishedDelegate = null;

	//登录
	public void Login(string username, string password)
	{
		//发起http请求获得token，user id
		string uri = HTTP_LOGIN_SERVER_ADDRESS;

		NetParam param = new NetParam();
		param.SetParam((byte)LoginType.NETWORK_LOGIN, uri, username, password);
		readerResponse.Add(param);

		Dictionary<string, string> post = new Dictionary<string, string>();
		post.Add("logic","0");
		post.Add("uname",username);
		post.Add("pw",password);
        //by te on 20170616
        HttpConnection.Instance.Connect(uri, post, HttpSuccess);
        //StartCoroutine(HttpGet(uri,post));

    }

	//注册
	public void Regist(string username, string password)
	{
		//发起http请求获得token，user id
		string uri = HTTP_LOGIN_SERVER_ADDRESS;

		NetParam param = new NetParam();
		param.SetParam((byte)LoginType.NETWORK_REGIST, uri, username, password);
		readerResponse.Add(param);

		Dictionary<string, string> post = new Dictionary<string, string>();
		post.Add("logic","1");
		post.Add("uname",username);
		post.Add("pw",password);
        //by te on 20170616
        HttpConnection.Instance.Connect(uri, post, HttpSuccess);

        //StartCoroutine(HttpGet(uri,post));
	}

	public void HttpSuccess(WWW p_www){
        string data = p_www.text;
        if (readerResponse.Count <= 0) { return; }
		NetParam param = readerResponse[0];
		byte type = (byte)param.type;
		string username = param.usrname;
		string password = param.password;

		readerResponse.RemoveAt(0);

		NetTools.Log(data);
		JsonData jsonData = JsonMapper.ToObject(data);

		if(jsonData["code"].ToString() != "1"){
			HttpFailed(jsonData["msg"].ToString());
			return;
		}

		if (type == (byte)LoginType.NETWORK_LOGIN)
		{
			Uid = long.Parse(jsonData["datas"]["id"].ToString());
			UName = jsonData["datas"]["uname"].ToString();
			Token = jsonData["datas"]["token"].ToString();
            //ServerConnection.Instance.serverTime = long.Parse(jsonData["datas"]["tokentime"].ToString());

			//这里记录一下用户帐号密码，用于下次自动登录或者填充输入框
			PlayerPrefs.SetString(USERNAME_FIELD, param.usrname);
			PlayerPrefs.SetString(PASSWORD_FIELD, param.password);
			NetTools.Log("NETWORK_LOGIN : " + data);

			if(httpFinishedDelegate != null){
				httpFinishedDelegate();
			}

		}else if (type == (byte)LoginType.NETWORK_REGIST
			|| type == (byte)LoginType.NETWORK_QUICKLY){
			NetTools.Log("NETWORK_REGIST : " + data);
			//直接调用登录接口
			Login(username,password);
		}

	}

	public void HttpFailed(string err){
        //FuiController.Tips(("title_1"), err, null, null);
		NetTools.Log("Http Error : " + err);
	}

	public NetParam GetUserRecord(){
		string username = PlayerPrefs.GetString("username");
		string password = PlayerPrefs.GetString("password");

		NetParam param = new NetParam();
		param.SetParam((byte)LoginType.NETWORK_LOGIN, "", username, password);
		return param;
	}

	//获取上次登录的用户名
	public static string GetUNameRecord(){
		if(PlayerPrefs.HasKey(USERNAME_FIELD)){
			return PlayerPrefs.GetString(USERNAME_FIELD);
		}
		return "";
	}
	//获取上次登录的密码
	public static string GetPWRecord(){
		if(PlayerPrefs.HasKey(PASSWORD_FIELD)){
			return PlayerPrefs.GetString(PASSWORD_FIELD);
		}
		return "";
	}
}
