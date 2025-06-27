using UnityEngine;
using UnityEngine.Rendering;

public class EscenarioManager : MonoBehaviour
{
    public Transform[] bloquesEscenario; // Asigna aqu� los 4 bloques en el Inspector
    public float velocidad = 5f;         // Velocidad del desplazamiento
    public float limiteX = 15f;          // L�mite a la derecha para reposicionar
    public float distanciaEntreBloques = 0.08f; // Espacio entre bloques, puedes ajustar seg�n tu escena

   
    void Update()
    {
        foreach (Transform bloque in bloquesEscenario)
        {
            // Mover el bloque a la derecha (sentido negativo en la vista del jugador)
            bloque.Translate(Vector3.right * velocidad * Time.deltaTime);

            // Si pasa el l�mite, lo reposicionamos al final
            if (bloque.position.x >= limiteX)
            {
                // Buscar el bloque m�s a la izquierda
                Transform ultimoBloque = ObtenerUltimoBloque();
                float nuevaX = ultimoBloque.position.x - distanciaEntreBloques;
                bloque.position = new Vector3(nuevaX, bloque.position.y, bloque.position.z);
            }
        }
    }

    Transform ObtenerUltimoBloque()
    {
        Transform ultimo = bloquesEscenario[0];
        foreach (Transform bloque in bloquesEscenario)
        {
            if (bloque.position.x < ultimo.position.x)
            {
                ultimo = bloque;
            }
        }
        return ultimo;
    }
}
