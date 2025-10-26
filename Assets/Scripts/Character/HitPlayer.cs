using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer : MonoBehaviour
{
    public float radioGolpe = 0.5f; // Radio de detección del golpe
    public LayerMask capaEnemigo; // Capa de los enemigos
    public Transform puntoGolpe; // Punto donde se genera el golpe
    public int daño = 2; // Daño que inflige el jugador

    public void IntentarGolpear()
    {
        print("Intentando golpear...");
        Collider2D enemigo = Physics2D.OverlapCircle(puntoGolpe.position, radioGolpe, capaEnemigo);

        if (enemigo != null)
        {
            print("¡Golpe al enemigo!");

            // Aplicar daño al enemigo
            VidaEnemigo saludEnemigo = enemigo.GetComponent<VidaEnemigo>();
            if (saludEnemigo != null)
            {
                saludEnemigo.RecibirDaño(daño);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (puntoGolpe != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(puntoGolpe.position, radioGolpe);
        }
    }
}
