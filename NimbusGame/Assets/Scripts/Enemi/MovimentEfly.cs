using System.Collections;
using UnityEngine;

public class MovimentEfly : MonoBehaviour
{
    public Animator ani;
    public GameObject target;
    public bool atacando;
    public int direccion;
    public int rutina;
    public float cronometro;
    public float speed_walk;
    public float speed_run;
   
    public float rango_vision;
    public float rango_vision_vertical;
    public float rango_ataque;
    
    public float cooldownGolpe = 1.0f;
    private float tiempoUltimoGolpe = 0;
    public HitEnemi hitScript;

    private Rigidbody2D rb;
    private Vector3 posicionInicial;
    public HealtEnemi healt;
    public float tiempoParaDestruir = 2f;
    public bool muerto = false;

    void Start()
    {
        ani = GetComponent<Animator>();
        target = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        posicionInicial = transform.position;
        rb.gravityScale = 0;
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), target.GetComponent<Collider2D>(), true);
    }

    void Update()
    {
        if(muerto == false) { 
            DetectarJugador();
            Comportamientos();
        }
    }

    void DetectarJugador()
    {
        float distanciaX = transform.position.x - target.transform.position.x;
        float distanciaY = transform.position.y - target.transform.position.y;

        // Verificar si el jugador está dentro del rango de visión en ambos ejes
        if (Mathf.Abs(distanciaX) < rango_vision && Mathf.Abs(distanciaY) < rango_vision_vertical)
        {
            // Si el jugador está detrás, girar hacia él
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
            if (distanciaX < rango_vision && distanciaY < rango_vision_vertical)
            {
                if (distanciaX < rango_ataque || distanciaY < rango_ataque)
                {
                    if (Time.time >= tiempoUltimoGolpe + cooldownGolpe)
                    {
                            StartCoroutine(IniciarAtaque());
                    }
                    else
                    {
                        ani.SetBool("Bat-Atack", false);
                        ani.SetBool("Bat-Fly", false);
                        ani.SetBool("Bat-RunFly", false);
                    }
                }
                else
                {
                    ani.SetBool("Bat-Fly", false);
                    ani.SetBool("Bat-RunFly", true);
                    Move(speed_run);
                    ani.SetBool("Bat-Atack", false);

                }
            }
            else
            {
                ani.SetBool("Bat-RunFly", false);
                cronometro += Time.deltaTime;
                if (cronometro >= 4)
                {
                    rutina = Random.Range(0, 2);
                    cronometro = 0;
                }
                switch (rutina)
                {
                    case 0:
                        ani.SetBool("Bat-Fly", false);
                        rb.velocity = new Vector2(0, rb.velocity.y);
                        break;
                    case 1:
                        direccion = Random.Range(0, 2);
                        rutina++;
                        break;
                    case 2:
                        MoveRut(speed_walk);
                        ani.SetBool("Bat-Fly", true);
                        break;
                }
            }
        }
    }

    void MoveRut(float speed)
    {
        float moveDirection = (direccion == 0) ? 1 : -1;
        rb.velocity = new Vector2(moveDirection * speed, rb.velocity.y);

        if (moveDirection > 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }
    void Move(float speed)
    {
        Vector2 direccionMovimiento = (target.transform.position - transform.position).normalized;
        rb.velocity = new Vector2(direccionMovimiento.x * speed, direccionMovimiento.y * speed);

        if (direccionMovimiento.x > 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }



    void ChangeDirection()
    {
        direccion = (direccion == 0) ? 1 : 0;
    }

    IEnumerator IniciarAtaque()
    {
        atacando = true; // Bloquear movimiento mientras ataca
        rb.velocity = Vector2.zero; // Detener movimiento
        ani.SetBool("Bat-Atack", true);
        ani.SetBool("Bat-Fly", false);
        ani.SetBool("Bat-RunFly", false);

        yield return new WaitForSeconds(0.0f); // Delay antes de golpear

        // Verificar si ha pasado el tiempo de cooldown desde el último golpe
        if (Time.time >= tiempoUltimoGolpe + cooldownGolpe)
        {
            hitScript?.DetectarGolpe(); // Ejecutar el golpe
            tiempoUltimoGolpe = Time.time; // Registrar el momento del último golpe
        }

        yield return new WaitForSeconds(0.5f); // Delay después de golpear

        ani.SetBool("Bat-Atack", false);
        atacando = false; // Permitir moverse nuevamente
    }

    public void Morir()
    {
        muerto = true;
        rb.velocity = Vector2.zero;
        ani.SetBool("Bat-Dead", true);
        ani.SetBool("Bat-Atack", false);
        ani.SetBool("Bat-Fly", false);
        ani.SetBool("Bat-RunFly", false);
        this.enabled = false;
        StartCoroutine(DestruirDespuesDeTiempo());
    }

    IEnumerator DestruirDespuesDeTiempo()
    {
        yield return new WaitForSeconds(tiempoParaDestruir);
        Destroy(gameObject);
    }

    public void ReiniciarPosicion()
    {
        transform.position = posicionInicial;
        healt.ResetHealth();
    }
}
