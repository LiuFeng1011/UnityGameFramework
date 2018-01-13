using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.IO;
using System.Text;
using UnityEditor.ProjectWindowCallback;
using System.Text.RegularExpressions;

public class CreateScrept {
	
	public static void OnWillCreateAsset (string _path)
	{
		Debug.Log("_path 1: " + _path);
		_path = _path.Replace(".meta", "");
		int tIndex = _path.LastIndexOf(".");
		string tFile = _path.Substring(tIndex);
		if(tFile != ".cs" && tFile != ".js" && tFile != ".boo")
		{
			return;
		}

		tIndex = Application.dataPath.LastIndexOf("Assets");
		_path = Application.dataPath.Substring(0, tIndex) + _path;
		tFile = System.IO.File.ReadAllText(_path);
		Debug.Log("_path 2: " + _path);
//		tFile = tFile.Replace("#CREATEDATE#", System.DateTime.Now.ToString("d")).Replace("#DEVELOPER#", PlayerSettings.companyName); 
//
//		System.IO.File.WriteAllText(_path, tFile);
//		AssetDatabase.Refresh();
	}


}
