using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("UI de Puntaje")]
    public TextMeshProUGUI scoreText; // Asigna esto en el inspector

    private int score = 0;

    void Awake()
    {
        // Singleton
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void SumarPuntaje(int cantidad)
    {
        score += cantidad;
        ActualizarUI();
    }

    void ActualizarUI()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    public int GetPuntaje()
    {
        return score;
    }
}
