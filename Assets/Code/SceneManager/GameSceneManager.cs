using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

public class GameSceneManager : BaseUnityObject 
{
    public delegate void CallBack_1(SceneTag tag);
    public enum SceneTag 
    {
        Gate,
        Logo,
        Menu,
        Game,
        Loading
    }
    /// <summary>
    /// 目标场景
    /// </summary>
    private static SceneTag m_tag = SceneTag.Gate;
    /// <summary>
    /// 根据标签跳转场景
    /// </summary>
    /// <param name="tag"></param>
    private static void ChangeScene(SceneTag tag)
    {
        LoadScene(SceneTag.Loading);
        m_tag = tag;
    }

    /// <summary>
    /// Loading场景调用方法
    /// </summary>
    private static void LoadingScene()
    {
        Debuger.Log("GameSceneManager loading scene : " + m_tag);
        LoadScene(m_tag);
    }

    /// <summary>
    /// 异步加载跳转场景
    /// </summary>
    /// <param name="name"></param>
    private static void LoadSceneAsync(string name)
    {
        SceneManager.LoadSceneAsync(name);
    }
    private static void LoadScene(string name) 
    {
        SceneManager.LoadScene(name);
    }
    private static void LoadScene(SceneTag tag)
    {
        LoadScene(Enum.GetName(typeof(SceneTag), tag));
    }
    private static void LoadSceneAsync(SceneTag tag)
    {
        LoadSceneAsync(Enum.GetName(typeof(SceneTag), tag));
    }


    private static List<CallBack_1> CallBackList;

    private static void ExChangeSceneCallBack()
    {
        if (CallBackList == null)
        {
            CallBackList = new List<CallBack_1>();
        }
        for (int i = 0; i < CallBackList.Count; i++)
        {
            CallBackList[i](m_tag);
        }
        //if(FuiController.Enable)
        //FuiController.OpenSceneLoading();
    }

    private static void RegisterCallBack(CallBack_1 callback) 
    {
        if (CallBackList == null) 
        {
            CallBackList = new List<CallBack_1>();
        }

        CallBackList.Add(callback);
    }
    private static void RemoveCallBack(CallBack_1 callback)
    {
        if (CallBackList == null)
        {
            CallBackList = new List<CallBack_1>();
        }

        CallBackList.Remove(callback);
    }


    static GameSceneManager _instance = null;
    public static GameSceneManager Instance { get { return _instance; } }

    void Awake()
    {
        if (_instance == null) _instance = this;
        else {
            Destroy(this);
            return;
        }
        EventManager.Register(this,
                              EventID.EVENT_SCENE_CHANGE,
                              EventID.EVENT_SCENE_LOADING_FINISHED);
        Debuger.Log("===========初始化场景管理器==========");
    }

    public override void HandleEvent(EventData resp)
    {

        switch(resp.eid){
            case EventID.EVENT_SCENE_CHANGE:
                EventChangeScene eve = (EventChangeScene)resp;
                ChangeScene(eve.stag);
                break;
            case EventID.EVENT_SCENE_LOADING_FINISHED:
                LoadingScene();
                break;

        }

    }
    private void OnDestroy()
    {
        EventManager.Remove(this);
    }
}
