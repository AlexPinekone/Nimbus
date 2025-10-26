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
    public int dano = 1;

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
    public bool TocandoSuelo = false;

    void Start()
    {
        ani = GetComponent<Animator>();
        target = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        dano = 1;
        posicionInicial = transform.position;
        rb.gravityScale = 0;
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), target.GetComponent<Collider2D>(), true);
    }

    void Update()
    {
        if (muerto && !TocandoSuelo)
        {
            rb.gravityScale = 1f;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.2f, LayerMask.GetMask("Ground"));
            if (hit.collider != null)
            {
                TocandoSuelo = true;
            }
        }

        if (!muerto)
        {
            if (DetectarJugador())
            {
                Comportamientos();
            }
            else
            {
                // Si no está en rango, cancelar animaciones de ataque o persecución
                ani.SetBool("Bat-RunFly", false);
                ani.SetBool("Bat-Atack", false);
                ani.SetBool("Bat-Fly", false);

                // Patrulla por defecto
                cronometro += Time.deltaTime;
                if (cronometro >= 4)
                {
                    rutina = Random.Range(0, 2);
                    cronometro = 0;
                }

                switch (rutina)
                {
                    case 0:
                        rb.velocity = new Vector2(0, rb.velocity.y);
                        ani.SetBool("Bat-Fly", false);
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

    bool DetectarJugador()
    {
        float distanciaX = transform.position.x - target.transform.position.x;
        float distanciaY = transform.position.y - target.transform.position.y;

        if (Mathf.Abs(distanciaX) < rango_vision && Mathf.Abs(distanciaY) < rango_vision_vertical)
        {
            if ((distanciaX > 0 && direccion == 0) || (distanciaX < 0 && direccion == 1))
            {
                ChangeDirection();
            }
            return true;
        }

        return false;
    }

    void Comportamientos()
    {
        float distanciaX = Mathf.Abs(transform.position.x - target.transform.position.x);
        float distanciaY = Mathf.Abs(transform.position.y - target.transform.position.y);

        if (!atacando)
        {
            if (distanciaX < rango_ataque && distanciaY < rango_ataque)
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
        Vector2 direction = (target.transform.position - transform.position).normalized;
        rb.velocity = direction * speed_run;

        // Rotar hacia el jugador
        if (direction.x > 0)
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
        atacando = true;
        rb.velocity = Vector2.zero;
        ani.SetBool("Bat-Atack", true);
        ani.SetBool("Bat-Fly", false);
        ani.SetBool("Bat-RunFly", false);

        yield return new WaitForSeconds(0.0f);

        if (Time.time >= tiempoUltimoGolpe + cooldownGolpe)
        {
            hitScript?.DetectarGolpe();
            tiempoUltimoGolpe = Time.time;
        }

        yield return new WaitForSeconds(0.5f);

        ani.SetBool("Bat-Atack", false);
        atacando = false;
    }

    public void Morir()
    {
        Destroy(gameObject);
        /*
        muerto = true;
        dano = 0;
        rb.velocity = Vector2.zero;
        ani.SetBool("Bat-Dead", true);
        ani.SetBool("Bat-Atack", false);
        ani.SetBool("Bat-Fly", false);
        ani.SetBool("Bat-RunFly", false);
        StartCoroutine(DestruirDespuesDeTiempo());
        */
    }

    /*IEnumerator DestruirDespuesDeTiempo()
    {
        yield return new WaitForSeconds(tiempoParaDestruir);
        Destroy(gameObject);
    }*/

    public void ReiniciarPosicion()
    {
        transform.position = posicionInicial;
        healt.ResetHealth();
    }
}
