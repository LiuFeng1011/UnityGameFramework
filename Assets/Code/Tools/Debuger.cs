/**
	1.Debuger.log可以在发布的时候关闭所有的Log输出。
	2.因为定位问题,将Debuger下的所有代码注释，而使用Debuger.dll,日志定位就不会定位到当前文件中了,会直接定位到输出的位置。
	3.生成Debuger.dll命令: mcs -r:/Applications/Unity/Unity.app/Contents/Frameworks/Managed/UnityEngine.dll -target:library Debuger.cs
	  mcs => Mono C# compiler
*/


using UnityEngine;
using System.Collections;

public class Debuger  {
	
	static public bool EnableLog = false;
	static public void Log(object message)
	{
		Log(message,null);
	}
	static public void Log(object message, Object context)
	{
		if(EnableLog)
		{
			Debug.Log(message,context);
		}
	}
	static public void LogError(object message)
	{
		LogError(message,null);
	}
	static public void LogError(object message, Object context)
	{
		if(EnableLog)
		{
			Debug.LogError(message,context);
		}
	}
	static public void LogWarning(object message)
	{
		LogWarning(message,null);
	}
	static public void LogWarning(object message, Object context)
	{
		if(EnableLog)
		{
			Debug.LogWarning(message,context);
		}
	}
}
