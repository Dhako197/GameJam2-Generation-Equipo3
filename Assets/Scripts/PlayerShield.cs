using UnityEngine;
using System.Collections;

public class PlayerShield : MonoBehaviour
{
    [Header("Objeto de part�cula del escudo (GameObject, no solo ParticleSystem)")]
    public GameObject escudoParticulaGO;

    [Header("Duraci�n del escudo")]
    public float duracionEscudo = 5f;

    private Coroutine rutinaEscudo;

    public void ActivarEscudo()
    {
        // Si ya hay una corrutina corriendo, la detenemos para reiniciar el escudo
        if (rutinaEscudo != null)
        {
            StopCoroutine(rutinaEscudo);
        }

        rutinaEscudo = StartCoroutine(ActivarYDesactivarEscudo());
    }

    private IEnumerator ActivarYDesactivarEscudo()
    {
        // Activar GameObject con la part�cula
        if (escudoParticulaGO != null)
            escudoParticulaGO.SetActive(true);

        yield return new WaitForSeconds(duracionEscudo);

        // Desactivar GameObject con la part�cula
        if (escudoParticulaGO != null)
            escudoParticulaGO.SetActive(false);

        rutinaEscudo = null;
    }
}
