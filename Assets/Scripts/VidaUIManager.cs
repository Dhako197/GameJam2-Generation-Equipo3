using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class VidaUIManager : MonoBehaviour
{
    public List<RawImage> iconosVida; // Asignar en el Inspector
    private int vidasRestantes;

    void Start()
    {
        vidasRestantes = iconosVida.Count;
        ActualizarUI();
    }

    public void QuitarVida()
    {
        if (vidasRestantes <= 0) return;

        vidasRestantes--;

        if (vidasRestantes >= 0 && vidasRestantes < iconosVida.Count)
        {
            iconosVida[vidasRestantes].enabled = false;
        }

        if (vidasRestantes <= 0)
        {
            Debug.Log("¡Jugador sin vidas!");
            // Aquí puedes llamar a un GameOver o algo más
        }
    }

    public void ActualizarUI()
    {
        for (int i = 0; i < iconosVida.Count; i++)
        {
            iconosVida[i].enabled = i < vidasRestantes;
        }
    }
}

