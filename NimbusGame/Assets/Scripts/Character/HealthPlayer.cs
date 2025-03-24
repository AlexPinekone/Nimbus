using UnityEngine;
using UnityEngine.UI;  // Necesario para manejar UI

public class HealthPlayer : MonoBehaviour
{
    public int vidaMaxima = 7;
    public int vidaActual;

    // Referencia al objeto de la vida dentro de UI (el que cambia de sprite)
    public Image vidaUI;

    // Array con los 7 sprites correspondientes a la vida
    public Sprite[] spritesVida;

    void Start()
    {
        vidaActual = vidaMaxima;
        ActualizarBarraVida();
    }

    public void RecibirDaño(int daño)
    {
        vidaActual -= daño;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);
        ActualizarBarraVida();

        if (vidaActual <= 0)
        {
            print("¡Jugador muerto!");
            // Aquí podrías agregar la lógica de muerte
        }
    }

    void ActualizarBarraVida()
    {
        if (vidaUI != null && spritesVida.Length == 7)
        {
            int indice = Mathf.Clamp(vidaActual, 0, 6);  // Asegura que el índice esté dentro de los 7 sprites
            vidaUI.sprite = spritesVida[indice];
        }
    }
}
