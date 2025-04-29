using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Drag[] pieces; // Tüm parçalar
    [SerializeField] private GameObject levelCompletePanel; // Seviye tamamlanma paneli
    [SerializeField] private GameObject sparkleFX; // Sparkle efekti

    private bool levelCompleted = false;
    private LevelUIManager uiManager; // UI Manager referansý

    void Start()
    {
        // LevelUIManager'ý ayný GameObject'ten al
        uiManager = GetComponent<LevelUIManager>();
        if (uiManager == null)
        {
            Debug.LogError("LevelUIManager is not found on this GameObject!");
        }

        // Paneli baþlangýçta gizle
        if (levelCompletePanel != null)
        {
            levelCompletePanel.SetActive(false);
            Debug.Log("LevelCompletePanel initialized: Hidden");
        }
        else
        {
            Debug.LogError("LevelCompletePanel is not assigned!");
        }

        // Sparkle FX'i baþlangýçta gizle
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

    void Update()
    {
        // Eðer seviye zaten tamamlandýysa, kontrol etme
        if (levelCompleted) return;

        // Tüm parçalarýn yerleþtirilip yerleþtirilmediðini kontrol et
        bool allPlaced = true;
        foreach (Drag piece in pieces)
        {
            if (!piece.IsPlaced)
            {
                allPlaced = false;
                break;
            }
        }

        // Eðer tüm parçalar yerleþtirildiyse
        if (allPlaced)
        {
            levelCompleted = true;
            if (uiManager != null)
            {
                uiManager.DisableSettingsButton(); // Hemen Settings butonunu devre dýþý býrak
                Debug.Log("Level complete, disabling Settings button");
            }
            StartCoroutine(ShowLevelCompletePanelWithDelay());
        }
    }

    private IEnumerator ShowLevelCompletePanelWithDelay()
    {
        Debug.Log("Activating SparkleFX");
        if (sparkleFX != null)
        {
            ParticleSystem ps = sparkleFX.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                var main = ps.main;
                main.useUnscaledTime = true; // Time.timeScale'dan etkilenmesin
                main.loop = true; // Sürekli oynasýn
                sparkleFX.SetActive(true);
                ps.Play(); // Efekti oynat
            }
            else
            {
                sparkleFX.SetActive(true);
            }
        }
        Debug.Log("Waiting 2 seconds before showing LevelCompletePanel");
        yield return new WaitForSecondsRealtime(3f); // 2 saniye bekle
        if (levelCompletePanel != null && uiManager != null)
        {
            levelCompletePanel.SetActive(true);
            uiManager.PauseGame(); // Oyunu duraklat
            Debug.Log("Showing LevelCompletePanel and pausing game");
        }
    }

    public Drag[] Pieces => pieces;
}