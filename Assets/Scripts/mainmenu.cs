using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject creditsPanel;

    public void PlayGame()
    {
        SceneManager.LoadScene("Cyber Menss");
    }

    public void ShowCredits()
    {
        mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void BackToMenu()
    {
        creditsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}
