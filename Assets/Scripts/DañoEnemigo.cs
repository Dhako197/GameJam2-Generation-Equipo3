using FirstGearGames.SmoothCameraShaker;
using UnityEngine;

public class DañoEnemigo : MonoBehaviour
{
    //public enum TipoPeligro { Enemigo, Proyectil, Obstaculo }
    //public TipoPeligro tipo;
    public int puntajeARestar = 5;
    public AudioClip hitAudio;
    public AudioClip AudioEscudo;
    public ShakeData hitShake;
   
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.Instance?.SumarPuntaje(-puntajeARestar);

            // Quitar una vida visual
            VidaUIManager vidaUI = FindObjectOfType<VidaUIManager>();
            vidaUI?.QuitarVida();
            
            PlayerVfX playerVfX= other.GetComponent<PlayerVfX>();
            if(playerVfX != null) playerVfX.PlayParticles();
            CameraShakerHandler.Shake(hitShake);
            
            // desactivar Objeto
            AudioManager.Instance.PlaySound(hitAudio);
            gameObject.SetActive(false);
        }
        if (other.CompareTag("Escudo"))
        {
            gameObject.SetActive(false);
            AudioManager.Instance.PlaySound(AudioEscudo);
            CameraShakerHandler.Shake(hitShake);
        }
    }
}
