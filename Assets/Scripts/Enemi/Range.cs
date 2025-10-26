using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    // Referencia al Animator para controlar las animaciones del enemigo
    public Animator ani;

    // Referencia al script de movimiento del enemigo (MovimentE)
    public MovimentE enemigo;

    /// Método que se ejecuta cuando un objeto entra en el trigger del collider 2D.
    void OnTriggerEnter2D(Collider2D coll)
    {
        // Verificar si el objeto que entra en el trigger tiene el tag "Player"
        if (coll.CompareTag("Player"))
        {
            // Desactivar las animaciones de caminar y correr
            ani.SetBool("Walk", false);
            ani.SetBool("Run", false);

            // Activar la animación de ataque
            ani.SetBool("Attack", true);

            // Indicar que el enemigo está atacando
            enemigo.atacando = true;

            // Desactivar el collider para evitar múltiples detecciones
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
