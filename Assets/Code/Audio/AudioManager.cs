using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

/// <summary>
/// 使用
/// 播放背景音乐
/// AudioManager.Instance.PlayBG("Bgm_run");
/// 播放音效
/// AudioManager.Instance.Play("UIbuild_button");
/// </summary>
public class AudioManager : BaseUnityObject
{
	public static bool loadFinished = false;

    private static AudioManager instance = null;
    public static AudioManager Instance { get { return instance; } }

	string _audioPath = "Prefabs/AudioSources/";//声音资源路径
    string _audioSwitchKey = "Sound";//音效开关键值
    string _musicSwitchKey = "Music";//音乐开关键值
    AudioSource _mSource;
    Transform _transform;


    /// <summary>
    /// 音效开关(0关闭，1打开)
    /// </summary>
	int audioSize = -1;
    public int AudioSize
    {
        get
        {
			if (audioSize == -1)
            {
				if(PlayerPrefs.HasKey(_audioSwitchKey)) {
					audioSize = PlayerPrefs.GetInt(_audioSwitchKey);
				}else{
					audioSize = 1;
				}
            }
			return audioSize;
		}set 
		{ 
			audioSize = value;
			PlayerPrefs.SetInt(_audioSwitchKey, value );
		}
        //set { PlayerPrefs.SetInt(_audioSwitchKey, value); }
    }
    /// <summary>
    /// 音乐开关(0关闭，1打开)
	/// </summary>
	int musicSize = -1;
    public int MusicSize
    {
        get { 
			if (musicSize == -1)
			{
				if(PlayerPrefs.HasKey(_musicSwitchKey)) {
					musicSize = PlayerPrefs.GetInt(_musicSwitchKey);
				}else{
					musicSize = 1;
				}
			}
			return musicSize;
		}set 
        { 
			musicSize = value;
            PlayerPrefs.SetInt(_musicSwitchKey, value);
			_mSource.volume = value / 100f;
        }
    }

    void Awake()
    {
        Debuger.Log("============初始化声音管理系统=============");
        if (!PlayerPrefs.HasKey(_musicSwitchKey))
        {
            PlayerPrefs.SetInt(_musicSwitchKey, 100);
        }
        if (!PlayerPrefs.HasKey(_audioSwitchKey))
        {
            PlayerPrefs.SetInt(_audioSwitchKey, 100);
        }


        if (instance != null && instance != this) { Destroy(this.gameObject); }
        else { instance = this; }
        DontDestroyOnLoad(this.gameObject);

        _transform = transform;
        _mSource =_transform.GetComponent<AudioSource>();
        if (_mSource == null)
        {
            _mSource = gameObject.AddComponent<AudioSource>();
		}
        loadFinished = true;
        Debuger.Log("----------------声音管理系统初始化完成----------------");
    }

    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="p_fileName"></param>
    public void PlayBG(string p_fileName)
    {
		if (_mSource.clip == null || p_fileName != _mSource.clip.name || !_mSource.isPlaying)
        {
			AudioClip t_clip = Resources.Load(_audioPath + p_fileName) as AudioClip;
            _mSource.clip = t_clip;
			_mSource.volume = MusicSize;
			_mSource.loop = true;
            _mSource.Play();
        }
    }

	/// <summary>
	/// 结束背景音乐
	/// </summary>
    public void StopBG()
    {
		if(_mSource != null)
			_mSource.Stop();
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="p_fileName"></param>
    public AudioSource Play(string p_fileName)
    {
        return Play(p_fileName, null, 1, false);
    }


    public AudioSource Play(string p_fileName, bool p_loop)
    {
        return Play(p_fileName, null, 1, p_loop);
    }
    public AudioSource Play(string p_fileName, Transform p_emitter)
    {
        return Play(p_fileName, p_emitter, 1, false);
    }
    public AudioSource Play(string p_fileName, float p_volume)
    {
        return Play(p_fileName, null, p_volume, false);
    }

    public AudioSource Play(string p_fileName, Transform p_emitter, float p_volume, bool p_loop)
    {
        if (AudioSize > 0)
        {
			AudioClip t_clip = Resources.Load(_audioPath + p_fileName) as AudioClip;
			if(t_clip == null ) return null;
            GameObject t_go = new GameObject("Audio:" + t_clip.name);
            t_go.transform.parent = p_emitter != null ? p_emitter : _transform;
            t_go.transform.localPosition = Vector3.zero;

            AudioSource t_source = t_go.AddComponent<AudioSource>();
            t_source.clip = t_clip;
			t_source.volume = AudioSize / 100f;
            t_source.loop = p_loop;
            if (!p_loop)
            {
                //t_go.AddComponent<AutoDestory>();
                Destroy(t_go, 50);
            }
            t_source.Play();

            return t_source;
        }
        return null;
    }

}