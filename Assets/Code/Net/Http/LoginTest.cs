using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginTest : MonoBehaviour {
	const int height = 30;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI()  
	{
		int high = 10;
		if(GUI.Button (new Rect (10, high,100,height), "login"))  
		{  
			//登录
			LoginManager.Instance().httpFinishedDelegate = LoginSuccess;
			LoginManager.Instance().Login("a5678900","123");
		}  

		high += height+5;

		if(GUI.Button (new Rect (10, high,100,height), "reg"))  
		{  
			//注册
			LoginManager.Instance().httpFinishedDelegate = LoginSuccess;
			LoginManager.Instance().Regist("a5678900","123");
		}  
	}

	public void LoginSuccess(){
		NetTools.Log("LoginSuccess uid : " + LoginManager.Uid);
		NetTools.Log("LoginSuccess token : " + LoginManager.Token);
		//这里可以登录服务器了
	}
}
