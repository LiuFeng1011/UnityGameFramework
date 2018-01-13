using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HttpDat
{
    public string url { get; private set; }
    public Dictionary<string, string> post { get; private set; }
    public Action<WWW> callback { get; private set; }

    public HttpDat(string p_url, Dictionary<string, string> p_post, Action<WWW> p_callback)
    {
        url = p_url;
        post = p_post;
        if (post == null) post = new Dictionary<string, string>();
        callback = p_callback;
    }
}
public class HttpConnection : MonoBehaviour
{
    bool _conneting = false;
    Action<WWW> _httpCallback;
    List<HttpDat> _httpDatPool = new List<HttpDat>();

    /// <summary>
    /// Http接口
    /// </summary>
    /// <param name="p_url"></param>
    /// <param name="p_post"></param>
    /// <param name="p_callback"></param>
    public void Connect(string p_url, Dictionary<string, string> p_post, Action<WWW> p_callback)
    {
        _httpDatPool.Add(new HttpDat(p_url, p_post, p_callback));
    }
    public void Connect(string p_url, Action<WWW> p_callback)
    {
        Connect(p_url, null, p_callback);
    }

    void SendHttpCallback(WWW p_www) { if (_httpCallback != null) _httpCallback(p_www); _httpCallback = null; _conneting = false; }

    //POST请求(Form表单传值、效率低、安全 ，)  
    IEnumerator HttpPost(string url, Dictionary<string, string> post)
    {
        //        FuiController.Loading();
        //表单   
        WWWForm form = new WWWForm();
        //从集合中取出所有参数，设置表单参数（AddField()).  
        foreach (KeyValuePair<string, string> post_arg in post)
        {
            form.AddField(post_arg.Key, post_arg.Value);
        }
        //表单传值，就是post   
        WWW www = new WWW(url, form);

        yield return www;

        if (www.error != null)
        {
            //POST请求失败  
            //NetTools.Log("Http Error : " + www.error);
			Debug.Log("Http Error : " + www.error);
        }
        else
        {
            //POST请求成功  
            SendHttpCallback(www);
        }
        
        //        FuiController.CloseLoading();
    }

    //GET请求（url?传值、效率高、不安全 ）  
    IEnumerator HttpGet(string url, Dictionary<string, string> get)
    {
        //		if(!GameConst.OPEN_LOGIN ){
        //			if(httpFinishedDelegate != null){
        //				httpFinishedDelegate();
        //			}
        //			yield break;
        //		}
        //FuiController.Loading();

        string Parameters;
        bool first;
        if (get.Count > 0)
        {
            first = true;
            Parameters = "?";
            //从集合中取出所有参数，设置表单参数（AddField()).  
            foreach (KeyValuePair<string, string> post_arg in get)
            {
                if (first)
                    first = false;
                else
                    Parameters += "&";

                Parameters += post_arg.Key + "=" + post_arg.Value;
            }
        }
        else
        {
            Parameters = "";
        }

        //直接URL传值就是get  
        WWW www = new WWW(url + Parameters);
        yield return www;

        if (www.error != null)
        {
            //GET请求失败  
            NetTools.Log("Http Error : " + www.error + ":" + www.url);
        }
        else
        {
            //FuiController.CloseLoading();
            if (www.text == null || www.text == "")
            {
                NetTools.Log("Http Error : " + www.error);
            }
            else
            {
                //GET请求成功  
                SendHttpCallback(www);
            }

        }
    }

    private void Update()
    {
        if (_httpDatPool.Count > 0 && !_conneting)
        {
            _conneting = true;
            _httpCallback = _httpDatPool[0].callback;
            StartCoroutine(HttpGet(_httpDatPool[0].url, _httpDatPool[0].post));
            _httpDatPool.RemoveAt(0);
        }
    }

    static HttpConnection _instance = null;
    public static HttpConnection Instance { get { return _instance; } }
    void Awake()
    {
        Debuger.Log("===========初始化网络连接管理器===========");
        if (_instance == null) _instance = this;
        else Destroy(this);
        Debuger.Log("---------网络连接管理器初始化完成---------");
    }
}
