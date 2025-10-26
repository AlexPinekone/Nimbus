using System.Collections;
using UnityEngine;

public class MovimentE : MonoBehaviour
{
    // Variables de comportamiento y movimiento
    public int rutina;
    public float cronometro;
    public Animator ani;
    public int direccion;
    public float speed_walk;
    public float speed_run;
    public GameObject target;
    public bool atacando;

    // Variables de detecci�n del jugador
    public float rango_vision;
    public float rango_vision_vertical;
    public float rango_ataque;

    // Variables de detecci�n de colisiones
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    private bool isGrounded;

    public Transform wallCheck;
    public float wallCheckRadius = 0.2f;
    public LayerMask obstacleLayer;
    private bool wallDetected;

    // Variables de ataque
    public float cooldownGolpe = 1.0f; // Tiempo de espera entre golpes
    private float tiempoUltimoGolpe = 0; // Tiempo del �ltimo golpe
    public HitEnemi hitScript;

    // Variables internas
    private Rigidbody2D rb;

    void Start()
    {
        ani = GetComponent<Animator>();
        target = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();

        // Ignorar colisiones entre el enemigo y el jugador
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), target.GetComponent<Collider2D>(), true);
    }

    void Update()
    {
        // Verificar si el enemigo est� en el suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Verificar si el enemigo toca una pared o llega al final de la plataforma
        wallDetected = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, obstacleLayer);

        // Si detecta una pared o no est� en el suelo, cambiar de direcci�n
        if (wallDetected || !isGrounded)
        {
            ChangeDirection();
        }

        // Verificar la posici�n del jugador
        DetectarJugador();

        // Comportamiento del enemigo
        Comportamientos();
    }

    void DetectarJugador()
    {
        float distanciaX = transform.position.x - target.transform.position.x;
        float distanciaY = transform.position.y - target.transform.position.y;

        // Verificar si el jugador est� dentro del rango de visi�n en ambos ejes
        if (Mathf.Abs(distanciaX) < rango_vision && Mathf.Abs(distanciaY) < rango_vision_vertical)
        {
            // Si el jugador est� detr�s, girar hacia �l
            if ((distanciaX > 0 && direccion == 0) || (distanciaX < 0 && direccion == 1))
            {
                ChangeDirection();
            }
        }
    }

    public void Comportamientos()
    {
        float distanciaX = Mathf.Abs(transform.position.x - target.transform.position.x);
        float distanciaY = Mathf.Abs(transform.position.y - target.transform.position.y);

        if (!atacando)
        {
            // Si el jugador est� dentro del rango de visi�n horizontal y vertical
            if (distanciaX < rango_vision && distanciaY < rango_vision_vertical)
            {
                // Si el jugador est� dentro del rango de ataque
                if (distanciaX < rango_ataque)
                {
                    // Verificar si ha pasado el tiempo de cooldown
                    if (Time.time >= tiempoUltimoGolpe + cooldownGolpe)
                    {
                        StartCoroutine(IniciarAtaque());
                    }
                    else
                    {
                        // Si est� en cooldown, volver al estado idle
                        ani.SetBool("Attack", false);
                        ani.SetBool("Walk", false);
                        ani.SetBool("Run", false);
                    }
                }
                else // Si el jugador est� fuera del rango de ataque pero dentro del rango de visi�n
                {
                    ani.SetBool("Walk", false);
                    ani.SetBool("Run", true);
                    Move(speed_run);
                    ani.SetBool("Attack", false);
                }
            }
            else // Si el jugador est� fuera del rango de visi�n
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
        }
    }

    void Move(float speed)
    {
        float moveDirection = (direccion == 0) ? 1 : -1; // Determina la direcci�n
        rb.velocity = new Vector2(moveDirection * speed, rb.velocity.y);

        // Cambiar la direcci�n del sprite
        if (moveDirection > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0); // Mirar a la derecha
        }
        else if (moveDirection < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0); // Mirar a la izquierda
        }
    }

    void ChangeDirection()
    {
        // Cambiar direcci�n
        direccion = (direccion == 0) ? 1 : 0;
    }

    IEnumerator IniciarAtaque()
    {
        atacando = true; // Bloquear movimiento mientras ataca
        rb.velocity = Vector2.zero; // Detener movimiento
        ani.SetBool("Attack", true);
        ani.SetBool("Walk", false);
        ani.SetBool("Run", false);

        yield return new WaitForSeconds(0.0f); // Delay antes de golpear

        // Verificar si ha pasado el tiempo de cooldown desde el �ltimo golpe
        if (Time.time >= tiempoUltimoGolpe + cooldownGolpe)
        {
            hitScript?.DetectarGolpe(); // Ejecutar el golpe
            tiempoUltimoGolpe = Time.time; // Registrar el momento del �ltimo golpe
        }

        yield return new WaitForSeconds(0.5f); // Delay despu�s de golpear

        ani.SetBool("Attack", false);
        atacando = false; // Permitir moverse nuevamente
    }
}