using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{
    public GameObject optionsPanel;
	public GameObject creditsPanel;

    public void PlayGame()
    {
        // Cargar la pantalla de selecci�n de partida
        SceneManager.LoadScene("CinematicStart");
    }

    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }

    public void OpenCredits()
    {
	    //creditsPanel.SetActive(true);
	    //optionsPanel.SetActive(false);
	    SceneManager.LoadScene("Credits");
    }

    public void ClosePanels()
    {
	    //optionsPanel.SetActive(false);
	    //creditsPanel.SetActive(false);
	    SceneManager.LoadScene("Menu");
    }
    
	public void ReturnMenu(string nombreE)
	{
		//optionsPanel.SetActive(false);
		//creditsPanel.SetActive(false);
		SceneManager.LoadScene(nombreE);
	}

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Salir del juego");
    }
}
