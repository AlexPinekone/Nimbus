using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    // Referencia al Animator para controlar las animaciones del enemigo
    public Animator ani;

    // Referencia al script de movimiento del enemigo (MovimentE)
    public MovimentE enemigo;
    public MovimentEfly enemigoVolador;

    /// Método que se ejecuta cuando un objeto entra en el trigger del collider 2D.
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            // Desactivar animaciones de movimiento
            ani.SetBool("Walk", false);
            ani.SetBool("Run", false);
            ani.SetBool("Attack", true);

            // Marcar como atacando
            if (enemigo != null)
            {
                enemigo.atacando = true;
            }
            else if (enemigoVolador != null)
            {
                enemigoVolador.atacando = true;
            }

            // Desactivar el trigger temporalmente para evitar múltiples ataques
            GetComponent<BoxCollider2D>().enabled = false;

            // Si deseas aplicar daño al jugador, hazlo aquí
            HealtEnemi vida = coll.GetComponent<HealtEnemi>();
            if (vida != null)
            {
                vida.RecibirDaño(1, transform.position);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        enemigo = GetComponentInParent<MovimentE>();
        enemigoVolador = GetComponentInParent<MovimentEfly>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
