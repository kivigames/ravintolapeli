using System.Collections.Generic;
using System.Linq;
using GameToolkit.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [Header("Language")]
    [SerializeField]
    private string languagePreferenceName = "gameLanguage";

    [SerializeField]
    private TMP_Dropdown languageDropdown = null;

    [SerializeField]
    private LocalizedText languageName = null;

    [Header("Audio")]
    [SerializeField]
    private AudioMixer audioMixer = null;

    [SerializeField]
    private string musicVolumeParameter = "musicVolume";

    [SerializeField]
    private string musicPreferenceName = "musicVolume";

    [SerializeField]
    private Slider musicSlider = null;

    [SerializeField]
    private string soundVolumeParameter = "soundVolume";

    [SerializeField]
    private string soundPreferenceName = "soundVolume";

    [SerializeField]
    private Slider soundSlider = null;

    private void Start()
    {
        if (languageDropdown)
        {
            languageDropdown.ClearOptions();
            languageDropdown.AddOptions(GetLanguages());

            var lang = GetSavedLanguage();
            if (lang != Language.Unknown)
            {
                Localization.Instance.CurrentLanguage = lang;
                languageDropdown.value = GetLanguageIndex(lang);
            }

            languageDropdown.onValueChanged.AddListener(OnLanguageChanged);
        }

        if (musicSlider)
        {
            var savedValue = PlayerPrefs.GetFloat(musicPreferenceName, 1.0f);
            musicSlider.onValueChanged.AddListener(OnMusicSlider);
            musicSlider.value = savedValue;
        }

        if (soundSlider)
        {
            var savedValue = PlayerPrefs.GetFloat(soundPreferenceName, 1.0f);
            soundSlider.onValueChanged.AddListener(OnSoundSlider);
            soundSlider.value = savedValue;
        }
    }

    private List<string> GetLanguages()
    {
        return LocalizationSettings.Instance.AvailableLanguages.Select(lang =>
            languageName.TryGetLocaleValue(lang, out var localName) ? localName : lang.Name).ToList();
    }

    private int GetLanguageIndex(Language language)
    {
        return LocalizationSettings.Instance.AvailableLanguages.IndexOf(language);
    }

    private void OnLanguageChanged(int value)
    {
        Localization.Instance.CurrentLanguage = LocalizationSettings.Instance.AvailableLanguages[value];
        PlayerPrefs.SetString(languagePreferenceName, Localization.Instance.CurrentLanguage.Code);
        PlayerPrefs.Save();
    }

    private void OnMusicSlider(float value)
    {
        audioMixer.SetFloat(musicVolumeParameter, Utils.LinearToDecibel(value));
        PlayerPrefs.SetFloat(musicPreferenceName, value);
        PlayerPrefs.Save();
    }

    private void OnSoundSlider(float value)
    {
        audioMixer.SetFloat(soundVolumeParameter, Utils.LinearToDecibel(value));
        PlayerPrefs.SetFloat(soundPreferenceName, value);
        PlayerPrefs.Save();
    }

    private Language GetSavedLanguage()
    {
        if (PlayerPrefs.HasKey(languagePreferenceName))
        {
            var languageCode = PlayerPrefs.GetString(languagePreferenceName, "");
            var language =
                LocalizationSettings.Instance.AvailableLanguages.FirstOrDefault(x => x.Code == languageCode);

            if (language != null) return language;
        }

        Localization.Instance.SetSystemLanguage();

        return Localization.Instance.CurrentLanguage;
    }
}