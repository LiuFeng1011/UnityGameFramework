using UnityEngine;
using System.Collections;

public class GameBtnManager : BaseSingleton<GameBtnManager>{

	public static GameObject GetBt(string name){
		GameObject t_obj = null;
		//寻找方法获取按钮
		System.Reflection.MethodInfo methondinfo = instance.GetType().GetMethod(name);
		if(methondinfo != null){
			t_obj = (GameObject)methondinfo.Invoke(null,null);
		}
		return t_obj;
	}

	public static GameObject GetBtnByStrings(string parentName, string[] str){
		GameObject obj = GameObject.Find(parentName);
		if(str == null){
			return obj;
		}
		return GetBtnForeach(obj.transform,str,0);
	}

	//递归获取按钮
	public static GameObject GetBtnForeach(Transform t,string[] str,int count){

		if(count < str.Length-1){
			t = t.Find(str[count]);
			if(t == null) return null;
			count++;
			return GetBtnForeach(t,str,count);
		}else{
			return t.Find(str[count]).gameObject;
		}
	}

	//buttons 
	
	public static GameObject testBtn() { 
		return GetBtnByStrings("GameObject",new string[]{"a","b","c","Cube"}); 
	}

//	//右上角的主线任务快捷按钮
//	public static GameObject	mainTaskFastBtn	() { 
//		return GetBtnByStrings("UI Root",new string[]{"Panel_World","PlayerShow","playerCompass","FoundBtn"}); 
//	}
//	//右下角的接受任务按钮
//	public static GameObject	acceptTaskBtn	() {
//		return GetBtnByStrings("UI Root",new string[]{"Panel_Story(Clone)","withItem","sureBtn"}); 
//	}

}
