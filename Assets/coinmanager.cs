using UnityEngine;
using TMPro;  // Solo si usas TextMeshPro

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    public int coinCount = 0;

    public TextMeshProUGUI coinText; // ← Este campo debe aparecer

    void Awake()
    {
        instance = this;
    }

    public void AddCoin()
    {
        coinCount++;
        coinText.text = "Monedas: " + coinCount;
    }
}
