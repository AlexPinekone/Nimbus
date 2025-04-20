using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class CinematicControllerFinal : MonoBehaviour
{
	public Image imageDisplay; // la imagen en pantalla
	public Sprite[] panels;    // imágenes de la cinemática
	public AudioClip[] sounds; // sonidos por panel (opcional)
	public AudioSource audioSource;
	public float fadeDuration = 1f;
	public float displayTime = 3f;

	void Start()
	{
		StartCoroutine(PlayCinematic());
	}

	IEnumerator PlayCinematic()
	{
		for (int i = 0; i < panels.Length; i++)
		{
			imageDisplay.sprite = panels[i];
			yield return StartCoroutine(FadeIn());

			if (i < sounds.Length && sounds[i] != null)
			{
				audioSource.clip = sounds[i];
				audioSource.Play();
			}

			yield return new WaitForSeconds(displayTime);
			
			

			yield return StartCoroutine(FadeOut());
		}

		// Aquí puedes cargar la siguiente escena o continuar el juego
		Debug.Log("Cinemática terminada");
		
		
		SceneManager.LoadScene("Creditos");
		
	}

	IEnumerator FadeIn()
	{
		for (float t = 0; t < fadeDuration; t += Time.deltaTime)
		{
			float alpha = t / fadeDuration;
			SetAlpha(alpha);
			yield return null;
		}
		SetAlpha(1f);
	}

	IEnumerator FadeOut()
	{
		for (float t = 0; t < fadeDuration; t += Time.deltaTime)
		{
			float alpha = 1f - (t / fadeDuration);
			SetAlpha(alpha);
			yield return null;
		}
		SetAlpha(0f);
	}

	void SetAlpha(float alpha)
	{
		Color c = imageDisplay.color;
		c.a = alpha;
		imageDisplay.color = c;
	}
}
