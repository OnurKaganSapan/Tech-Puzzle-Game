using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LevelUIManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject levelCompletePanel;
    [SerializeField] private Button settingsButton;
    [SerializeField] private AudioSource buttonClickSound; // Buton týklama sesi
    [SerializeField] private GameObject sparkleFX; // Sparkle efekti

    private bool isPaused = false;

    void Start()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
            Debug.Log("Settings panel initialized: Hidden");
        }
        else
        {
            Debug.LogError("Settings panel is not assigned!");
        }

        if (levelCompletePanel != null)
        {
            levelCompletePanel.SetActive(false);
            Debug.Log("LevelCompletePanel initialized: Hidden");
        }
        else
        {
            Debug.LogError("LevelCompletePanel is not assigned!");
        }

        if (settingsButton != null)
        {
            settingsButton.interactable = true;
            Debug.Log("Settings button initialized: Interactable");
        }
        else
        {
            Debug.LogError("Settings button is not assigned!");
        }

        if (buttonClickSound == null)
        {
            Debug.LogError("ButtonClickSound is not assigned!");
        }

        if (sparkleFX != null)
        {
            sparkleFX.SetActive(false);
            Debug.Log("SparkleFX initialized: Hidden");
        }
        else
        {
            Debug.LogError("SparkleFX is not assigned!");
        }
    }

    public void OpenSettings()
    {
        if (buttonClickSound != null) buttonClickSound.Play();
        StartCoroutine(OpenSettingsWithDelay());
    }

    private IEnumerator OpenSettingsWithDelay()
    {
        Debug.Log("Settings button pressed, waiting 0.5 seconds...");
        yield return new WaitForSecondsRealtime(0.5f);
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
            PauseGame();
            Debug.Log("Settings panel opened, game paused");
        }
    }

    public void ResumeGame()
    {
        if (buttonClickSound != null) buttonClickSound.Play();
        StartCoroutine(ResumeGameWithDelay());
    }

    private IEnumerator ResumeGameWithDelay()
    {
        Debug.Log("Resume button pressed, waiting 0.5 seconds...");
        yield return new WaitForSecondsRealtime(0.5f);
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
            ResumeGameInternal();
            Debug.Log("Settings panel closed, game resumed");
        }
    }

    public void NextLevel()
    {
        if (buttonClickSound != null) buttonClickSound.Play();
        StartCoroutine(NextLevelWithDelay());
    }

    private IEnumerator NextLevelWithDelay()
    {
        Debug.Log("Next Level button pressed, waiting 0.5 seconds...");
        yield return new WaitForSecondsRealtime(0.5f);
        if (sparkleFX != null)
        {
            ParticleSystem ps = sparkleFX.GetComponent<ParticleSystem>();
            if (ps != null) ps.Stop();
            sparkleFX.SetActive(false);
            Debug.Log("SparkleFX stopped");
        }
        ResumeGameInternal();
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "MainScene")
        {
            SceneManager.LoadScene("MainScene 2");
        }
        else
        {
            SceneManager.LoadScene("Main-Menu-Example");
        }
    }

    public void RetryLevel()
    {
        if (buttonClickSound != null) buttonClickSound.Play();
        StartCoroutine(RetryLevelWithDelay());
    }

    private IEnumerator RetryLevelWithDelay()
    {
        Debug.Log("Retry button pressed, waiting 0.5 seconds...");
        yield return new WaitForSecondsRealtime(0.5f);
        if (sparkleFX != null)
        {
            ParticleSystem ps = sparkleFX.GetComponent<ParticleSystem>();
            if (ps != null) ps.Stop();
            sparkleFX.SetActive(false);
            Debug.Log("SparkleFX stopped");
        }
        ResumeGameInternal();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMainMenu()
    {
        if (buttonClickSound != null) buttonClickSound.Play();
        StartCoroutine(ReturnToMainMenuWithDelay());
    }

    private IEnumerator ReturnToMainMenuWithDelay()
    {
        Debug.Log("Main Menu button pressed, waiting 0.5 seconds...");
        yield return new WaitForSecondsRealtime(0.5f);
        if (sparkleFX != null)
        {
            ParticleSystem ps = sparkleFX.GetComponent<ParticleSystem>();
            if (ps != null) ps.Stop();
            sparkleFX.SetActive(false);
            Debug.Log("SparkleFX stopped");
        }
        ResumeGameInternal();
        SceneManager.LoadScene("Main-Menu-Example");
    }

    public void QuitGame()
    {
        if (buttonClickSound != null) buttonClickSound.Play();
        StartCoroutine(QuitGameWithDelay());
    }

    private IEnumerator QuitGameWithDelay()
    {
        Debug.Log("Quit button pressed, waiting 0.5 seconds...");
        yield return new WaitForSecondsRealtime(0.5f);
        ResumeGameInternal();
        SceneManager.LoadScene("Main-Menu-Example");
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        Drag.IsGamePaused = true;
        isPaused = true;
        if (settingsButton != null)
        {
            settingsButton.interactable = false;
            Debug.Log("Settings button disabled");
        }
    }

    public void DisableSettingsButton()
    {
        if (settingsButton != null)
        {
            settingsButton.interactable = false;
            Debug.Log("Settings button disabled immediately");
        }
    }

    private void ResumeGameInternal()
    {
        Time.timeScale = 1f;
        Drag.IsGamePaused = false;
        isPaused = false;
        if (settingsButton != null)
        {
            settingsButton.interactable = true;
            Debug.Log("Settings button enabled");
        }
    }
}