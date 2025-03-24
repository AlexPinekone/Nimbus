using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemi : MonoBehaviour
{
    // Radio del �rea de golpe
    public float radioGolpe = 0.5f;

    // Capa del jugador para detectar colisiones
    public LayerMask capaJugador;

    // Punto de referencia donde ocurre el golpe 
    public Transform puntoGolpe;

    // Tiempo de espera entre golpes
    public float cooldownGolpe = 1.0f;

    // Tiempo del �ltimo golpe
    private float tiempoUltimoGolpe = 0;

    // Cantidad de da�o que inflige el enemigo
	public int dano = 1; 

    void Update()
    {
        // Llamar al m�todo de detecci�n de golpe en cada frame
        DetectarGolpe();
    }

    /// Detecta si el golpe del enemigo impacta al jugador.
    public void DetectarGolpe()
    {
        // Verificar si ha pasado el tiempo de cooldown desde el �ltimo golpe
        if (Time.time >= tiempoUltimoGolpe + cooldownGolpe)
        {
            // Verificar si hay un collider del jugador dentro del �rea de golpe
            Collider2D jugador = Physics2D.OverlapCircle(puntoGolpe.position, radioGolpe, capaJugador);

            if (jugador != null)
            {

                // funci�n para aplicar da�o al jugador
                HealthPlayer saludJugador = jugador.GetComponent<HealthPlayer>();
                if (saludJugador != null)
                {
	                saludJugador.RecibirDano(dano);
                }

                // Registrar el momento del �ltimo golpe
                tiempoUltimoGolpe = Time.time;
            }
        }
    }

    /// Dibuja un gizmo en el Editor para visualizar el �rea de golpe.
    void OnDrawGizmosSelected()
    {
        if (puntoGolpe != null)
        {
            // color del gizmo a rojo
            Gizmos.color = Color.red;

            // Dibujar una esfera wireframe en la posici�n del punto de golpe
            Gizmos.DrawWireSphere(puntoGolpe.position, radioGolpe);
        }
    }
}