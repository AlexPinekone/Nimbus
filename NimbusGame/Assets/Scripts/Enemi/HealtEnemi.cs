using UnityEngine;

public class HealtEnemi : MonoBehaviour
{
    public int vida = 5; // Vida inicial del enemigo
    public float fuerzaRetroceso = 2f;
    public float tiempoRetroceso = 0.3f;

    private Rigidbody2D rb;
    private Animator ani;

    public MovimentE movimiento; // Para enemigos terrestres
    public MovimentEfly movimientoVolador; // Para enemigos voladores

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();

        // Asignar referencias automáticamente si existen
        movimiento = GetComponent<MovimentE>();
        movimientoVolador = GetComponent<MovimentEfly>();
    }

    public void RecibirDaño(int cantidad, Vector2 origen)
    {
        vida -= cantidad;
        print("¡El enemigo recibió daño! Vida restante: " + vida);

        // Retroceso
        Vector2 direccionRetroceso = (transform.position - (Vector3)origen).normalized;
        rb.velocity = Vector2.zero;
        rb.AddForce(direccionRetroceso * fuerzaRetroceso, ForceMode2D.Impulse);

        if (ani != null)
            ani.SetTrigger("Hit");

        // Marcar como atacando (inactivo temporalmente)
        if (movimiento != null)
            movimiento.atacando = true;
        else if (movimientoVolador != null)
            movimientoVolador.atacando = true;

        Invoke(nameof(FinRetroceso), tiempoRetroceso);

        if (vida <= 0)
        {
            Morir();
        }
    }

    void FinRetroceso()
    {
        if (movimiento != null)
            movimiento.atacando = false;
        else if (movimientoVolador != null)
            movimientoVolador.atacando = false;
    }

    void Morir()
    {
        print("¡El enemigo ha sido derrotado!");
        Destroy(gameObject);
    }
}
