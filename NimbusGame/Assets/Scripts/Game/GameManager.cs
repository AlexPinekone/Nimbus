using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	public Vector2 playerNewPosition;
	public bool changePosition = false;
	
	void Awake(){
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else{
			Destroy(gameObject);
		}
	}
	
	public void changeEscene(string levelName, Vector2 position)
	{
		playerNewPosition = position;
		changePosition = true;
		SceneManager.LoadScene(levelName);
	}
}
