using UnityEngine;

public class VidaEnemigo : MonoBehaviour
{
    public int vida = 5; // Vida inicial del enemigo

    public void RecibirDa�o(int cantidad)
    {
        vida -= cantidad;
        print("�El enemigo recibi� da�o! Vida restante: " + vida);

        if (vida <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        print("�El enemigo ha sido derrotado!");
        Destroy(gameObject);
    }
}
