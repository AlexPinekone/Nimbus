using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    public Animator ani;
    public MovimentE enemigo;

    void OnTrigerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            ani.SetBool("Walk", false);
            ani.SetBool("Run", false);
            ani.SetBool("Attack", true);
            enemigo.atacando = true;
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
