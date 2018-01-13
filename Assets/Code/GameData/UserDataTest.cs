using UnityEngine;
using System.Collections;

public class UserDataTest : MonoBehaviour {
	public string uname = "setname";
	// Use this for initialization
	void Start () {
		UserDataManager.instance.Start();

		Debug.Log("uid : " + UserDataManager.instance.GetUserid());
		Debug.Log("uname : " + UserDataManager.instance.GetUserName());
		Debug.Log("level : " + UserDataManager.instance.GetUserLevel());
		Debug.Log("viplevel : " + UserDataManager.instance.GetUserVipLevel());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI()  
	{  
		//GUI.color = Color.red;  

		if(GUI.Button (new Rect (150, 100, 100, 30), "changname"))  
		{  
			UserDataManager.instance.SetUserName(uname);
			Debug.Log("uname : " + UserDataManager.instance.GetUserName());
		}  

	} 


}
