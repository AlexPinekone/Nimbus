using UnityEngine;
using UnityEngine.UI;  // Necesario para manejar UI

public class HealthPlayer : MonoBehaviour
{
	public int vidaMaxima = 3;
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

	public void RecibirDano(int dano)
    {
	    vidaActual -= dano;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);
        ActualizarBarraVida();

        if (vidaActual <= 0)
        {
            print("�Jugador muerto!");
            // Aqu� podr�as agregar la l�gica de muerte
        }
    }

    void ActualizarBarraVida()
    {
	    if (vidaUI != null && spritesVida.Length == 4)
        {
		    int indice = Mathf.Clamp(vidaActual, 0, 3);  // Asegura que el �ndice est� dentro de los 7 sprites
            vidaUI.sprite = spritesVida[indice];
        }
    }
}
