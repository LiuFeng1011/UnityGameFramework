using UnityEngine;
using System.Collections;
using LitJson;

public class AHTestClass{
	public AHInt v = 199;
}

public class AHTest : MonoBehaviour {
	int normalval = 0;
	// Use this for initialization
	void Start () {
		AHTestClass c = new AHTestClass();
		AHInt test = 199;

		Debug.Log("ah test : " + (test - 9));
		test -= 10;
		normalval = test;
		Debug.Log("noraml value : " + normalval);
		Debug.Log("JSON value : " + JsonMapper.ToJson(c));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
