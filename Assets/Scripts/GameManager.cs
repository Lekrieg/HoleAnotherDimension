using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CamAndDoorPoints
{
	public Vector2 maxPosition;
	public Vector2 minPosition;

	public Vector2 doorPosition;
}

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	public CamAndDoorPoints[] cdPoints;

	public int door;

	public bool isInteracting = false;

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

		door = 0;
	}

	public void GoToScene(string name)
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(name);
	}
	public void GoToDoor(int door)
	{
		if(door < 2)
		{
			// Door is used to acess the area of the camera and the next door
			this.door = door;

			GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
			playerObj.transform.position = cdPoints[door].doorPosition;
		}
		else
		{
			// Fade out and load the credits
			// todo: continue the game
			Animator fadeAnim = GameObject.Find("Fade").GetComponent<Animator>();
			fadeAnim.SetTrigger("Fade");

			StartCoroutine(LoadCredits());
		}
	}

	IEnumerator LoadCredits()
	{
		yield return new WaitForSeconds(1);
		door = 0;
		UnityEngine.SceneManagement.SceneManager.LoadScene("Credits");
	}
}
