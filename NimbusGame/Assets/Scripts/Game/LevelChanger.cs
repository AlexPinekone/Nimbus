using UnityEngine;

public class LevelChanger : MonoBehaviour
{
	public string nextLevelName;
	public Vector2 DestinationPosition;
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			GameManager.instance.changeEscene(nextLevelName, DestinationPosition);
		}
	}
}
