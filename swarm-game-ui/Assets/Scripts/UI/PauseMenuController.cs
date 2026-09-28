using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Sagust kosedeki "II" (pause) butonuna basildiginda acilan menu.
/// Ekran goruntusundeki dort buton (Devam Et, Ayarlar, Ana Menu,
/// Oyundan Cik) burada yonetiliyor.
///
/// Oyunun backend/AI tarafi henuz gelistirme asamasinda oldugundan
/// (mentorumuzun talimatiyla), "Devam Et" su an icin sadece zamani
/// tekrar baslatiyor; gercek oyun ici durum (hangi arac secili, savas
/// devam ediyor mu vb.) AI/state machine tarafi hazir oldugunda buraya
/// baglanacak.
/// </summary>
public class PauseMenuController : MonoBehaviour
{
    [Header("Sahne Referanslari")]
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    private bool isPaused = false;

    /// <summary>Sagustteki "II" ikonuna baglanir.</summary>
    public void OnPauseButtonPressed()
    {
        isPaused = !isPaused;
        pauseMenuPanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }

    /// <summary>"Devam Et" butonu.</summary>
    public void OnResumePressed()
    {
        isPaused = false;
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    /// <summary>"Ayarlar" butonu - duraklatma menusunu gizleyip ayarlar panelini acar.</summary>
    public void OnSettingsPressed()
    {
        pauseMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    /// <summary>"Ana Menu" butonu - zamani normale dondurup ana menu sahnesine gecer.</summary>
    public void OnMainMenuPressed()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }

    /// <summary>"Oyundan Cik" butonu.</summary>
    public void OnQuitPressed()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
