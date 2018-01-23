using UnityEngine;
using System.Collections;

public class EventData {
    public EventID eid;

    public EventData(EventID eid){
        this.eid = eid;
    }

	public void Send(){
		EventManager.instance().SendEvent(this);
	}

    public static EventData CreateEvent(EventID eventid){
        EventData data = new EventData(eventid);
        return data;
    }

}

public class EventEntryGame : EventData{
    public EventEntryGame() : base(EventID.EVENT_ENTRYGAME){ }
	public string uname;
	public string password;
}


public class EventChangeScene : EventData
{
    public GameSceneManager.SceneTag stag;
    public EventChangeScene(GameSceneManager.SceneTag stag) : base(EventID.EVENT_SCENE_CHANGE) {
        this.stag = stag;
    }
}

public class EventTouch : EventData
{
    public Vector3 pos { get; private set; }
    public EventTouch(EventID eid, Vector3 pos) : base(eid)
    {
        this.pos = pos;
    }
}
