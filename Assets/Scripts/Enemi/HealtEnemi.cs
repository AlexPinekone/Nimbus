using UnityEngine;

public class HealtEnemi : MonoBehaviour
{
    public int vida = 1; // Vida inicial del enemigo
    public int vidaActual;
    public float fuerzaRetroceso = 2f;
    public float tiempoRetroceso = 0.3f;

    private Rigidbody2D rb;
    private Animator ani;
    public HitEnemi hitEnemi;

    public MovimentE movimiento; // Para enemigos terrestres
    public MovimentEfly movimientoVolador; // Para enemigos voladores
    public MovimentE Dead; // Para enemigos terrestres
    public MovimentEfly DeadVol; // Para enemigos voladores

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        vidaActual = vida;

        // Asignar referencias automáticamente si existen
        movimiento = GetComponent<MovimentE>();
        movimientoVolador = GetComponent<MovimentEfly>();
    }

    public void RecibirDaño(int cantidad, Vector2 origen)
    {
        vidaActual -= cantidad;
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

        if (vidaActual <= 0)
        {
            
            print("daño desactivado desde HealtEnemi");

            if (movimientoVolador != null)
                movimientoVolador.Morir();

            if (movimiento != null)
                movimiento.Morir();
        }
    }

    void FinRetroceso()
    {
        if (movimiento != null)
            movimiento.atacando = false;
        else if (movimientoVolador != null)
            movimientoVolador.atacando = false;
    }
   
    public void ResetHealth()
    {
        vidaActual = vida;  // Restaurar la vida
    }
}
