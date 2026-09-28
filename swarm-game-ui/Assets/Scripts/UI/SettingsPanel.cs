using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Ekran goruntusundeki Ayarlar paneli: "Ses" kaydiragi (slider) ve
/// "Geri" butonu. Ses degeri PlayerPrefs ile kaydedilir, boylece oyuncu
/// tekrar actiginda son ayari hatirlanir.
/// </summary>
public class SettingsPanel : MonoBehaviour
{
    private const string VOLUME_PREF_KEY = "MasterVolume";

    [Header("Sahne Referanslari")]
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private GameObject settingsPanelRoot;
    [SerializeField] private GameObject pauseMenuPanel;

    private void OnEnable()
    {
        // Panel her acildiginda, kaydedilmis son ses degerini goster
        float savedVolume = PlayerPrefs.GetFloat(VOLUME_PREF_KEY, 0.75f);
        volumeSlider.value = savedVolume;
        ApplyVolume(savedVolume);
    }

    /// <summary>Slider'a OnValueChanged olayi olarak baglanir.</summary>
    public void OnVolumeChanged(float newValue)
    {
        ApplyVolume(newValue);
        PlayerPrefs.SetFloat(VOLUME_PREF_KEY, newValue);
    }

    private void ApplyVolume(float value)
    {
        AudioListener.volume = Mathf.Clamp01(value);
    }

    /// <summary>"Geri" butonu - ayarlar panelini kapatip duraklatma menusune doner.</summary>
    public void OnBackPressed()
    {
        settingsPanelRoot.SetActive(false);
        pauseMenuPanel.SetActive(true);
    }
}
