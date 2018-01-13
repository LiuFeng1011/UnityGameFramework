using UnityEngine;
using System.Collections;

public class BaseUnityObject : MonoBehaviour,EventObserver {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public virtual void HandleEvent(EventData resp){

	}
}
