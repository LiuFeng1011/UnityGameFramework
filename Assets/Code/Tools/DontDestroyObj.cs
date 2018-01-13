using UnityEngine;
using System.Collections;

public class DontDestroyObj : MonoBehaviour {
	void Awake()
	{
		DontDestroyOnLoad(transform.gameObject);
	}
}
