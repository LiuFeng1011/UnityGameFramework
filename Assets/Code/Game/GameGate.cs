using UnityEngine;
using System.Collections;

public class GameGate : BaseUnityObject {
	public static bool loadFinished = false;

    GameObject gameHeart;
	// Use this for initialization
    void Start () {
        Debuger.EnableLog = true;

        gameHeart = new GameObject("GameHeart");
        gameHeart.AddComponent<DontDestroyObj>();

        //初始化数据存储系统
		UserDataManager.instance.Start();

        //初始化事件管理器
        EventManager em = gameHeart.AddComponent<EventManager>();

        //声音管理器
        AudioManager am = gameHeart.AddComponent<AudioManager>();

        //加载联网脚本
        HttpConnection hc = gameHeart.AddComponent<HttpConnection>();

        //初始化场景管理器
        GameSceneManager gsm = gameHeart.AddComponent<GameSceneManager>();


        EventManager.Register(this,EventID.EVENT_CONFIG_LOADFINISHED);
	}
	
	// Update is called once per frame
	void Update () {
		if(!loadFinished){
			if(EventManager.loadFinished && 
               AudioManager.loadFinished ){
                loadFinished = true;
				GameInitFinished();
            }
        }
	}

	//游戏系统初始化完成
	void GameInitFinished(){
        //启动更细配置表
        gameObject.AddComponent<ConfigUpdate>();
        ConfigUpdate.Instance.CheckConfig(ConfigUpdateCB);
	}

    //配置表更新完成
    void ConfigUpdateCB(){
        //加载配置表
        ConfigManager.LoadData();
    }

    //加载配置表完成
    void LoadConfigCB(){
        (new EventChangeScene(GameSceneManager.SceneTag.Logo)).Send();
    }

    public override void HandleEvent(EventData resp){
        switch(resp.eid){
            case EventID.EVENT_CONFIG_LOADFINISHED:
                LoadConfigCB();
                break;
        }
    }

    private void OnDestroy()
    {
        EventManager.Remove(this);
    }
}
