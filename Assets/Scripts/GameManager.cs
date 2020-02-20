using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	int level;

	void Awake()
	{
		if(instance != null)
		{
			Destroy(this.gameObject);
		}
		else
		{
			DontDestroyOnLoad(this.gameObject);
			instance = this;
		}

		level = 1;
	}

	public void NextLevel()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("0" + 1);
		level++;
	}
}
