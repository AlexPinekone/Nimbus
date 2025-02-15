using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentE : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public Animator ani;
    public int direccion;
    public float speed_walk;
    public float speed_run;
    public GameObject target;
    public bool atacando;

    public float rango_vision;
    public float rango_ataque;
    public GameObject rango;
    public GameObject Hit;

    // Detección de colisiones
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    private bool isGrounded;

    public Transform wallCheck;
    public float wallCheckRadius = 0.2f;
    public LayerMask obstacleLayer;
    private bool wallDetected;

    private Rigidbody2D rb;

    void Start()
    {
        ani = GetComponent<Animator>();
        target = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Verificar si el enemigo está en el suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Verificar si el enemigo toca una pared o llega al final de la plataforma
        wallDetected = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, obstacleLayer);

        // Si detecta una pared o no está en el suelo, cambiar de dirección
        if (wallDetected || !isGrounded)
        {
            ChangeDirection();
        }

        // Verificar la posición del jugador
        DetectarJugador();

        // Comportamiento del enemigo
        Comportamientos();
    }

    void DetectarJugador()
    {
        float distancia = transform.position.x - target.transform.position.x;

        if (Mathf.Abs(distancia) < rango_vision)
        {
            // Si el jugador está detrás, girar hacia él
            if ((distancia > 0 && direccion == 0) || (distancia < 0 && direccion == 1))
            {
                ChangeDirection();
            }
        }
    }

    public void Comportamientos()
    {
        float distancia = Mathf.Abs(transform.position.x - target.transform.position.x);

        if (distancia > rango_vision && !atacando)
        {
            ani.SetBool("Run", false);
            cronometro += Time.deltaTime;
            if (cronometro >= 4)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;
            }
            switch (rutina)
            {
                case 0:
                    ani.SetBool("Walk", false);
                    rb.velocity = new Vector2(0, rb.velocity.y); // Detenerse
                    break;
                case 1:
                    direccion = Random.Range(0, 2);
                    rutina++;
                    break;
                case 2:
                    Move(speed_walk);
                    ani.SetBool("Walk", true);
                    break;
            }
        }
        else
        {
            if (distancia > rango_ataque && !atacando)
            {
                ani.SetBool("Walk", false);
                ani.SetBool("Run", true);
                Move(speed_run);
                ani.SetBool("Attack", false);
            }
            else if (!atacando)
            {
                // Si está en rango de ataque, se detiene y ataca
                rb.velocity = new Vector2(0, rb.velocity.y);
                ani.SetBool("Walk", false);
                ani.SetBool("Run", false);
                ani.SetBool("Attack", true);
                atacando = true;
            }
        }
    }

    void Move(float speed)
    {
        float moveDirection = (direccion == 0) ? 1 : -1; // Determina la dirección
        rb.velocity = new Vector2(moveDirection * speed, rb.velocity.y); // Aplica movimiento

        // Cambiar la dirección del personaje y animación
        if (moveDirection > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0); // Mirar a la derecha
        }
        else if (moveDirection < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0); // Mirar a la izquierda
        }

        // Actualizar la animación según la velocidad
        ani.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }

    void ChangeDirection()
    {
        // Cambiar dirección
        direccion = (direccion == 0) ? 1 : 0;
    }

    public void Final_Ani()
    {
        ani.SetBool("Attack", false);
        atacando = false;
        rango.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void ColliderWeaponTrue()
    {
        Hit.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void ColliderWeaponFalse()
    {
        Hit.GetComponent<BoxCollider2D>().enabled = false;
    }
}
