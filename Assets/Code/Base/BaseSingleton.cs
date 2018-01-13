using UnityEngine;
using System.Collections;

public abstract class BaseSingleton<T> where T : BaseSingleton<T> , new(){

	private static T _instance = null;  

	public static T instance  
	{  
		get{
			if( _instance == null ){
				_instance = new T();

			}
			return _instance;
		}
	}  
}
