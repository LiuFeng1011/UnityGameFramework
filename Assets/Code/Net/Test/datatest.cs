using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
public class datatest : MonoBehaviour {
	Dictionary<int ,object> dic = new Dictionary<int ,object>();
	// Use this for initialization
	void Start () {
		dic.Add(1,1);
		dic.Add(2,"abc");
		dic.Add(3,23);


		Debug.Log("1 : " + dic[1]);
		Debug.Log("2 : " + dic[2]);
		Debug.Log("3 : " + dic[3]);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
