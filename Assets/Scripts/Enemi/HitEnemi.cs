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

    [SerializeField] private MovimentE movimentE;
    [SerializeField] private MovimentEfly movimentEfly;

    private void Start()
    {
        
    }

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
                    int dano = ObtenerDano();
                    saludJugador.RecibirDano(dano);
                    print(dano);
                }

                tiempoUltimoGolpe = Time.time;
            }
        }
    }

    private int ObtenerDano()
    {
        if (movimentE != null)
        {
            Debug.Log("Usando damage de MovementE: " + movimentE.dano);
            return movimentE.dano;
        }
        else if (movimentEfly != null)
        {
            Debug.Log("Usando damage de MovementEfly: " + movimentEfly.dano);
            return movimentEfly.dano;
        }
        else
        {
            Debug.LogWarning("NO HAY MovementE ni MovementEfly encontrados en el enemigo.");
            return 0;
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