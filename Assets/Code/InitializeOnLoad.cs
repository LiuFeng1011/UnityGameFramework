using UnityEngine;
using System.Collections;
using System.Runtime.Hosting;
using UnityEngine.SceneManagement;

public class InitializeOnLoad : MonoBehaviour {

	[RuntimeInitializeOnLoadMethod]
	static void Initialize()
	{
		if (SceneManager.GetActiveScene().name == "Gate")
		{
			return;
		}
        SceneManager.LoadScene("Gate");
	}
}
