using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System;
using System.IO;

/// <summary>
/// description:
/// 此类用于更新本地配置表
/// author:li zong bo
/// editor:liu feng on 170807
/// </summary>
public class ConfigUpdate : MonoBehaviour
{
	public static string configPath {get {return Application.persistentDataPath + "/config/";}}
    
	const string CONFIGURL = "http://www.dreamgear82.com/dreamgearconfig/config/excel/tankfire/";
    string infopath = CONFIGURL + "configinfo.txt";

    Action finishedCB;
    Action<string, int, int> _updateProgress;
    List<ConfigDat> _updateList = new List<ConfigDat>();//需要更新的配置文件

    /// <summary>
    /// 更新配置表总数量
    /// </summary>
    public int updateCount { get; private set; }

	public void AddFinishedEvent(Action p_action) { 
		finishedCB += p_action; 
	}
	public void RemoveFinishedEvent(Action p_action) { 
		finishedCB -= p_action; 
	}
	void UpdateFinished(){
		if(finishedCB != null) finishedCB();
	}

    public void AddUpdatePropressEvent(Action<string, int, int> p_action) { _updateProgress += p_action; }
    public void RemoveUpdatePropressEvent(Action<string, int, int> p_action) { _updateProgress -= p_action; }
    void SendUpdatePropressEvent(string p_name) { if (_updateProgress != null) _updateProgress(p_name, _updateList.Count, updateCount); }
    /// <summary>
    /// 检测配置表
    /// </summary>
	public void CheckConfig(Action finishedCB)
	{
		Debug.Log("检查配置表<<" + configPath);

		this.finishedCB = finishedCB;

		//文件夹是否存在
		//by te on 20170628 生成保存目录
		Directory.CreateDirectory(configPath);
		//te end
        _updateList.Clear();
        HttpConnection.Instance.Connect(infopath, CheckCallback);
    }

    void CheckCallback(WWW p_www)
    {
        Debug.Log(p_www.text);
        if (p_www.error != null)
        {
            //CheckConfig();
            return;
        }
        JsonData t_jd = JsonMapper.ToObject(p_www.text);
        ConfigDat[] t_data = new ConfigDat[t_jd.Count];
        for (int i = 0; i < t_jd.Count; i++)
        {
            t_data[i] = new ConfigDat(t_jd[i]);
            if (!t_data[i].Compare(ConfigUpdate.configPath))
            {
                _updateList.Add(t_data[i]);
            }
        }

		updateCount = _updateList.Count;
        Debug.Log("需要更新的配置表数量::" + _updateList.Count);
		UpdateConfig();
    }
    /// <summary>
    /// 更新配置表
    /// </summary>
    public void UpdateConfig()
    {
        if (_updateList.Count > 0)
        {
            HttpConnection.Instance.Connect(CONFIGURL + _updateList[0].name, DownloadCallback);
            SendUpdatePropressEvent(_updateList[0].name);
        }
        else
        {
            SendUpdatePropressEvent("last");
			UpdateFinished();
        }
    }
    void DownloadCallback(WWW p_www)
    {
        if (p_www.error != null)
        {
            UpdateConfig();
            return;
        }
		GameCommon.WriteByteToFile( p_www.bytes,configPath+_updateList[0].name);
        _updateList.RemoveAt(0);
        UpdateConfig();
    }

    static ConfigUpdate _instance = null;
    public static ConfigUpdate Instance { get { return _instance; } }
    void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(this);
    }
}

public class ConfigDat
{
    public string name;
    public int size;
    public string md5;

    public ConfigDat(JsonData p_jd)
    {
        name = p_jd["name"].ToString();
        size = int.Parse(p_jd["size"].ToString());
        md5 = p_jd["md5"].ToString();
    }

    public bool Compare(string p_path)
    {
		string t_md5 = ToolBox.MD5Sum.GetMD5HashFromFile(p_path + name);
        if (t_md5 == md5)
            return true;
        else
            return false;
    }
}
