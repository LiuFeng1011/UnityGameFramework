using UnityEngine;
using System.Collections;

public class MousePositionTest : MonoBehaviour {
	Vector3 mousePositionOnScreen;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		NetTools.Log("=");
		mousePositionOnScreen = Input.mousePosition;   
		NetTools.Log(mousePositionOnScreen.ToString());
		mousePositionOnScreen =  Camera.main.ScreenToWorldPoint(mousePositionOnScreen);  

		NetTools.Log(mousePositionOnScreen.ToString());
	}
}
