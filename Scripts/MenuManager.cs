using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private AudioSource buttonClickSound; // Buton týklama sesi

    public void PlayGame()
    {
        if (buttonClickSound != null) buttonClickSound.Play();
        StartCoroutine(PlayGameWithDelay());
    }

    private IEnumerator PlayGameWithDelay()
    {
        Debug.Log("Play button pressed, waiting 0.5 seconds...");
        yield return new WaitForSecondsRealtime(0.5f); // 0.5 saniye bekle
        Debug.Log("Loading MainScene");
        SceneManager.LoadScene("MainScene"); // Seviye 1
    }

    public void QuitGame()
    {
        if (buttonClickSound != null) buttonClickSound.Play();
        StartCoroutine(QuitGameWithDelay());
    }

    private IEnumerator QuitGameWithDelay()
    {
        Debug.Log("Quit button pressed, waiting 0.5 seconds...");
        yield return new WaitForSecondsRealtime(0.5f); // 0.5 saniye bekle
        Debug.Log("Game Quit");
        Application.Quit();
    }
}