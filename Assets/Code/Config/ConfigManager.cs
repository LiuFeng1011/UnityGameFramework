using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml;
using System.IO;

using System.Reflection;


/**
 *Y游戏配置表更新流程
 * 配置表url
 * const stirng CONFIGURL = "http://hideseek.dreamgear82.com/hideseek/config/excel/";
 * 1.获取配置表信息,地址:
 * string infopath = CONFIGURL+"configinfo.txt";
 * 数据样例:
 * [{"name":"constant.xml","size":2351,"md5":"1c739d23906a8e9a30767d9459a83839"},{"name":"dailytask.xml","size":3699,"md5":"92f093f57a5f5a7397d870195ac15d5f"}]
 * 2.根据配置表信息的name和md5 与本地文件进行比较，筛选出需要更新的配置表文件列表，进行更新
 * 3.下载配置表
 * 下载指定配置表文件:CONFIGURL+name;
 */

/**
 * 游戏配置表管理类
 * 
 * 添加配置表流程
 * 1.新建配置表管理脚本 
 * 2.创建数据类 xxxData ，属性字段与配置表一一对应
 * 3.创建数据管理类 xxxDataManager，
 * 4.实现管理类的load方法
 * 5.在ConfigManager中定义静态变量，在LoadData方法中调用管理类的Load方法
 */
public class ConfigManager {
    
//	public static ConfigManager instance;
//
//	public static ConfigManager GetInstance(){
//		if(instance == null){
//			instance = new ConfigManager();
//		}
//		return instance;
//	}

	public static bool loadDown = false;

	public static TankListManager tankListManager = new TankListManager();
	public static WeaponListManager weaponListManager = new WeaponListManager();
	public static SkillListManager skillListManager = new SkillListManager();
	public static ItemListManager itemListManager = new ItemListManager();
	public static NormalLevelManager normalLevelManager = new NormalLevelManager();
	public static ConfEffectManager confEffectManager = new ConfEffectManager();

    public static void LoadData(){
        Debuger.Log("===========启动配置表管理器===========");
		tankListManager.Load();
		weaponListManager.Load();
		skillListManager.Load();
		itemListManager.Load();
		normalLevelManager.Load();
        confEffectManager.Load();
        Debuger.Log("----------配置表管理器启动成功-----------");
        EventData.CreateEvent(EventID.EVENT_CONFIG_LOADFINISHED).Send();
    }

	public static List<T> Load<T>() where T : new(){
		
		string[] names = (typeof(T)).ToString().Split('.');

		string filename = names[names.Length - 1];
		Debug.Log("------filename : " + filename);
		XmlDocument doc = new XmlDocument();

        //by te on 20170616
		doc.Load(ConfigUpdate.configPath + filename + ".xml");

        //doc.Load(Application.dataPath + "/Resources/Config/" + filename + ".xml");

        XmlNode xmlNode = doc.DocumentElement;

		XmlNodeList xnl = xmlNode.ChildNodes;

		List<T> ret = new List<T>();

		foreach (XmlNode xn in xnl)
		{
			if (xn.Name.ToLower() == filename)
			{
				T obj = new T();

				Type t = obj.GetType();

				FieldInfo[] fields = t.GetFields();

				string msg = "";
				try{
					foreach (FieldInfo field in fields){
						string val = xn.Attributes[field.Name].Value;
						if(val == null){
							Debug.Log("the field [" + field.Name + "] is null !!!" );
							continue;
						}

						msg = field.Name + " : "+  val + "   type : " + field.FieldType;
						//Debug.Log(field.Name + " : "+  val + "   type : " + field.FieldType);
                        if (field.FieldType == typeof(int))
                        {
							field.SetValue(obj, int.Parse(val));
                        }
                        else if (field.FieldType == typeof(float))
                        {
                            field.SetValue(obj, float.Parse(val));
                        }
                        else if (field.FieldType == typeof(string))
                        {
                            field.SetValue(obj, val);
                        }

					}	
					ret.Add(obj);
				}catch(Exception e){
					Debug.LogError("=====================" + filename + "==================");
					Debug.LogError(e.Message);
					Debug.LogError(msg);
				}

			}
		}

		return ret;
	}



}
