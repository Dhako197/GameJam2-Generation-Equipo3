using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject textFinJuego;
    private bool juegoPausado = false;

    void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Opcional
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void FinJuego()
    {

        Time.timeScale = 0f;
        juegoPausado = true;
        AudioListener.pause = true; // Pausa el audio tambi�n
        textFinJuego.SetActive(true);

    }
    public void PausarJuego()
    {
        Time.timeScale = 0f;
        juegoPausado = true;
        AudioListener.pause = true; // Pausa el audio tambi�n
    }

    public void ReanudarJuego()
    {
        Time.timeScale = 1f;
        juegoPausado = false;
        AudioListener.pause = false;
    }

    public bool EstaPausado()
    {
        return juegoPausado;
    }

    // Puedes usar este m�todo si quieres alternar con una tecla
    public void TogglePausa()
    {
        if (juegoPausado)
            ReanudarJuego();
        else
            PausarJuego();
    }
}
