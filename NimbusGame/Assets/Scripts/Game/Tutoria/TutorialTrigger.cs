using UnityEngine;
using TMPro;
using System.Collections;

public class TutorialTrigger : MonoBehaviour
{
	public TextMeshProUGUI tutorialText;
	public TextMeshProUGUI nextText;
	public float fadeDuration = 0.5f;
	public bool bandera = false;
    
    void Start()
    {
	    SetAlpha(1f);
	    SetAlphaNext(0f);
    }
    
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player") && !bandera)
		{
			StartCoroutine(FadeOut());
		}
	}
	
	// Corutina que reduce el alfa gradualmente
	IEnumerator FadeOut()
	{
		float elapsed = 0f;
		Color originalColor = tutorialText.color;
		Color originalNextColor = nextText.color;

		while (elapsed < fadeDuration)
		{
			elapsed += Time.deltaTime;
			float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
			SetAlpha(alpha);
			float alphaN = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);
			SetAlphaNext(alphaN);
			
			yield return null;
		}

		SetAlpha(0f);
		SetAlphaNext(1f);
		bandera = true;
	}
	
	// Función para cambiar solo el alfa del texto
	void SetAlpha(float alpha)
	{
		Color c = tutorialText.color;
		c.a = alpha;
		tutorialText.color = c;
	}
	
	void SetAlphaNext(float alpha)
	{
		Color c = nextText.color;
		c.a = alpha;
		nextText.color = c;
	}
}
