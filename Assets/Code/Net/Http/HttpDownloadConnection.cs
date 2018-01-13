using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Net;
using System.Threading;  

/// <summary>
/// 由于www方式下载二进制数据流会出现数据传输错误，
/// 所以这里重新封装一个使用HttpWebRequest来下载的对象
/// 
/// 此对象只可下载数据时使用
/// </summary>
public class HttpDownloadConnection{
	public delegate void CallBack(bool issuccess);
	private static HttpDownloadConnection instance;

	private string downurl;
	private string saveurl;
	private string savename;
	private CallBack callback;

	/// <summary>
	/// 开始一个下载请求
	/// </summary>
	/// <param name="downurl">下载地址.</param>
	/// <param name="saveurl">存贮路径，包括文件名.</param>
	/// <param name="callback">回调.</param>
	public static void StartDownload(string downurl,string saveurl,string savename,
		CallBack callback = null){
		if(instance == null){
			instance = new HttpDownloadConnection();
		}

		instance.downurl = downurl;
		instance.saveurl = saveurl;
		instance.savename = savename;
		instance.callback = callback;

		instance.StartDownload();
	}

	void StartDownload(){
		Thread thread = new Thread(new ThreadStart(DownloadHttp));  
		thread.IsBackground = true;  
		thread.Start();  
	}

	/// <summary>
	/// 开启新线程进行下载
	/// </summary>
	void DownloadHttp(){
		
		HttpWebRequest request = (HttpWebRequest)WebRequest.Create(downurl);
		WebResponse response =  request.GetResponse();
		try
		{
			Stream inStream = response.GetResponseStream();
			int bufferSize = 1024;
			int readCount;
			byte[] buffer = new byte[bufferSize];
			readCount = inStream.Read(buffer, 0, bufferSize);
			//by te on 20170628 生成保存目录
			Directory.CreateDirectory(saveurl);
			//te end
			Stream outStream = File.Create(saveurl+savename);
			while (readCount > 0)
			{
				outStream.Write(buffer, 0, readCount);
				readCount = inStream.Read(buffer, 0, bufferSize);
			}

			inStream.Close();
			outStream.Close();
		}
		catch (Exception ex)
		{
			Debug.Log(ex.ToString());
			request.Abort();

			if(callback != null){
				callback(false);
			}
			return;
		}

		response.Close();
		request.Abort();
		if(callback != null){
			callback(true);
		}
	}
}
