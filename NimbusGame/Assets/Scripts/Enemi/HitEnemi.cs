using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemi : MonoBehaviour
{
    public float radioGolpe = 0.5f;
    public LayerMask capaJugador;
    public Transform puntoGolpe;
    public float cooldownGolpe = 1.0f;
    private float tiempoUltimoGolpe = 0;

    public int dano = 1;

    void Update()
    {
        DetectarGolpe();
    }

    // Detecta si el golpe del enemigo impacta al jugador.
    public void DetectarGolpe()
    {
        if (Time.time >= tiempoUltimoGolpe + cooldownGolpe)
        {
            Collider2D jugador = Physics2D.OverlapCircle(puntoGolpe.position, radioGolpe, capaJugador);

            if (jugador != null)
            {
                HealthPlayer saludJugador = jugador.GetComponent<HealthPlayer>();
                if (saludJugador != null)
                {
                    saludJugador.RecibirDano(dano);
                }

                tiempoUltimoGolpe = Time.time;
            }
        }
    }

    
    void OnDrawGizmosSelected()
    {
        if (puntoGolpe != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(puntoGolpe.position, radioGolpe);
        }
    }
}
