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
        // Cargar la pantalla de selección de partida
        SceneManager.LoadScene("CinematicStart");
    }

    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }

    public void OpenCredits()
    {
        creditsPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public void ClosePanels()
    {
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Salir del juego");
    }
}
