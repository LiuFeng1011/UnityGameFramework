using UnityEngine;
using System.Collections;

public class EventTest : MonoBehaviour ,EventObserver{
	public string uname = "aaa";
	public string password = "bbb";
	// Use this for initialization
	void Start () {
		EventManager.Register(this,EventID.EVENT_ENTRYGAME);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI () 
	{
		if (GUI.Button (new Rect (10,10,150,100), "send eve")) 
		{
			EventEntryGame eve = new EventEntryGame();
			eve.uname = uname;
			eve.password = password;
			eve.Send();
		}
	}

	public void HandleEvent(EventData data){
		EventEntryGame eve = (EventEntryGame)data;
		Debug.Log(eve.uname);
		Debug.Log(eve.password);

	}

	void OnDestroy(){
		EventManager.Remove(this);
	}
}
