using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemi : MonoBehaviour
{
    public Collider2D hitboxCollider; // Asigna el collider específico en el Inspector

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player") && coll.isTrigger)
        {
            print("Daño");
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
