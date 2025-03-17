using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{
	public Animator anim;
	
	private IEnumerator Fading(string sceneName)
	{
		//anim.Play("IFadeIn");
		anim.SetBool("Fade",true);
		//yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
		yield return new WaitForSeconds(1);
		SceneManager.LoadScene(sceneName);
		anim.SetBool("Fade",false);
	}

	public void LoadScene(string sceneName)
	{
		StartCoroutine(Fading(sceneName));
	}
}
