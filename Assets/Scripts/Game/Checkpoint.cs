using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public MovimentE resetEnemi;
    public MovimentEfly resetEnemiFly;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.SetCheckpoint(transform.position);

            if (resetEnemi != null )
            {
                resetEnemi.ReiniciarPosicion();
            }

            if (resetEnemiFly != null)
            {
                resetEnemiFly.ReiniciarPosicion();
            }
        }
    }
}

