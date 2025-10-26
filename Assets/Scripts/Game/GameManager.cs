using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Vector2 playerNewPosition;
    public bool changePosition = false;

    // Último checkpoint
    private Vector2 lastCheckpointPosition;
    private bool checkpointSet = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void changeEscene(string levelName, Vector2 position)
    {
        playerNewPosition = position;
        changePosition = true;
        SceneManager.LoadScene(levelName);
    }

    // Establecer el checkpoint
    public void SetCheckpoint(Vector2 position)
    {
        lastCheckpointPosition = position;
        checkpointSet = true;
        Debug.Log("✅ Checkpoint guardado: " + position);
    }

    // Obtener el checkpoint
    public Vector2 GetCheckpoint()
    {
        if (checkpointSet)
        {
            return lastCheckpointPosition;
        }
        else
        {
            // Si no hay checkpoint, devolver una posición inicial por defecto (0,0)
            return Vector2.zero;
        }
    }
}