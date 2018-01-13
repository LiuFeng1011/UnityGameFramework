using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debuger.Log("LoadingScene ");
        EventData.CreateEvent(EventID.EVENT_SCENE_LOADING_FINISHED).Send();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
