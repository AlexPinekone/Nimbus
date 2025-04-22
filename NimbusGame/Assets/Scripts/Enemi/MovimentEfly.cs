using System.Collections;
using UnityEngine;

public class MovimentEfly : MonoBehaviour
{
    public Animator ani;
    public float speed_fly;
    public GameObject target;
    public bool atacando;

    public float rango_vision;
    public float rango_vision_vertical;
    public float rango_ataque;

    public float cooldownGolpe = 1.0f;
    private float tiempoUltimoGolpe = 0;
    public HitEnemi hitScript;

    private Rigidbody2D rb;

    void Start()
    {
        ani = GetComponent<Animator>();
        target = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();

        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), target.GetComponent<Collider2D>(), true);
    }

    void Update()
    {
        DetectarJugador();
        Comportamientos();
    }

    void DetectarJugador()
    {
        float distanciaX = transform.position.x - target.transform.position.x;

        // Girar hacia el jugador si está detrás
        if ((distanciaX > 0 && transform.localScale.x > 0) || (distanciaX < 0 && transform.localScale.x < 0))
        {
            Flip();
        }
    }

    void Comportamientos()
    {
        float distanciaX = Mathf.Abs(transform.position.x - target.transform.position.x);
        float distanciaY = Mathf.Abs(transform.position.y - target.transform.position.y);

        if (!atacando)
        {
            if (distanciaX < rango_vision && distanciaY < rango_vision_vertical)
            {
                if (distanciaX < rango_ataque && distanciaY < rango_ataque)
                {
                    if (Time.time >= tiempoUltimoGolpe + cooldownGolpe)
                    {
                        StartCoroutine(IniciarAtaque());
                    }
                    else
                    {
                        ani.SetBool("Attack", false);
                        ani.SetBool("Walk", false);
                    }
                }
                else
                {
                    ani.SetBool("Walk", true);
                    ani.SetBool("Attack", false);
                    FlyTowardsTarget();
                }
            }
            else
            {
                ani.SetBool("Walk", false);
                rb.velocity = Vector2.zero;
            }
        }
    }

    void FlyTowardsTarget()
    {
        Vector2 direction = (target.transform.position - transform.position).normalized;
        rb.velocity = direction * speed_fly;
    }

    void Flip()
    {
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

    IEnumerator IniciarAtaque()
    {
        atacando = true;
        rb.velocity = Vector2.zero;
        ani.SetBool("Attack", true);
        ani.SetBool("Walk", false);

        yield return new WaitForSeconds(0.0f);

        if (Time.time >= tiempoUltimoGolpe + cooldownGolpe)
        {
            hitScript?.DetectarGolpe();
            tiempoUltimoGolpe = Time.time;
        }

        yield return new WaitForSeconds(0.5f);

        ani.SetBool("Attack", false);
        atacando = false;
    }
}