using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
	public static GameManager instance;
	public static PlayerManager instancePlayer;

	// Start is called before the first frame update
	void Awake()
	{
		/*
		if (instancePlayer == null)
		{
			instancePlayer = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}*/
	}
	
    void Start()
	{
		if (GameManager.instance.changePosition)
		{
			transform.position = GameManager.instance.playerNewPosition;
			GameManager.instance.changePosition = false;
		}
        
    }
}
