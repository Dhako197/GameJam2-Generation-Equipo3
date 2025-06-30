using UnityEngine;

public class DañoEnemigo : MonoBehaviour
{
    //public enum TipoPeligro { Enemigo, Proyectil, Obstaculo }
    //public TipoPeligro tipo;
    public int puntajeARestar = 5;
   
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.Instance?.SumarPuntaje(-puntajeARestar);

            // Quitar una vida visual
            VidaUIManager vidaUI = FindObjectOfType<VidaUIManager>();
            vidaUI?.QuitarVida();
            // desactivar Objeto
            gameObject.SetActive(false);
        }
        if (other.CompareTag("Escudo"))
        {
            gameObject.SetActive(false);
            Debug.Log("Player Protegido");
        }
    }
}
