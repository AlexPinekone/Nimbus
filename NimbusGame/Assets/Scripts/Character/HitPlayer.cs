using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer : MonoBehaviour
{
    public float radioGolpe = 0.5f; // Radio de detecci�n del golpe
    public LayerMask capaEnemigo; // Capa de los enemigos
    public Transform puntoGolpe; // Punto donde se genera el golpe
    public int da�o = 2; // Da�o que inflige el jugador

    public void IntentarGolpear()
    {
        print("Intentando golpear...");
        Collider2D enemigo = Physics2D.OverlapCircle(puntoGolpe.position, radioGolpe, capaEnemigo);

        if (enemigo != null)
        {
            print("�Golpe al enemigo!");

            // Aplicar da�o al enemigo
            VidaEnemigo saludEnemigo = enemigo.GetComponent<VidaEnemigo>();
            if (saludEnemigo != null)
            {
                saludEnemigo.RecibirDa�o(da�o);
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
