using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    [Header("Volume Setting")]
    [SerializeField] private TMP_Text volumeTextValue;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private float defaultVolume = 1.0f;

    [Header("Graphics Settings")]
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;

    [Header("Confirmation")]
    [SerializeField] private GameObject confirmationPrompt;

    [Header("Levels To Load")]
    public string newGameLevel;
    private string levelToLoad;
    [SerializeField] private GameObject noSavedGameDialog;

    private Resolution[] resolutions;

    private void Start()
    {
        LoadPlayerPrefs();
        LoadResolutions();
        LoadQualitySettings();
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0.0");
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        StartCoroutine(ShowConfirmationBox());
    }

    public void ResetButton(string setting)
    {
        switch (setting)
        {
            case "Audio":
                volumeSlider.value = defaultVolume;
                break;
            case "Graphics":
                qualityDropdown.value = QualitySettings.GetQualityLevel();
                resolutionDropdown.value = GetCurrentResolutionIndex();
                fullscreenToggle.isOn = Screen.fullScreen;
                break;
        }
    }

    public void NewGameDialogYes()
    {
        SceneManager.LoadScene(newGameLevel);
    }

    public void LoadGameDialogYes()
    {
        if (PlayerPrefs.HasKey("SavedLevel"))
        {
            levelToLoad = PlayerPrefs.GetString("SavedLevel");
            SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            noSavedGameDialog.SetActive(true);
        }
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    private void LoadPlayerPrefs()
    {
        if (PlayerPrefs.HasKey("masterVolume"))
        {
            float localVolume = PlayerPrefs.GetFloat("masterVolume");
            volumeTextValue.text = localVolume.ToString("0.0");
            volumeSlider.value = localVolume;
            AudioListener.volume = localVolume;
        }
    }

    private void LoadResolutions()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    private void LoadQualitySettings()
    {
        qualityDropdown.ClearOptions();

        List<string> options = new List<string>();
        string[] qualityNames = QualitySettings.names;

        int currentQualityIndex = 0;
        for (int i = 0; i < qualityNames.Length; i++)
        {
            options.Add(qualityNames[i]);

            if (qualityNames[i] == QualitySettings.names[QualitySettings.GetQualityLevel()])
            {
                currentQualityIndex = i;
            }
        }

        qualityDropdown.AddOptions(options);
        qualityDropdown.value = currentQualityIndex;
        qualityDropdown.RefreshShownValue();
    }

    private int GetCurrentResolutionIndex()
    {
        int resolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                resolutionIndex = i;
                break;
            }
        }
        return resolutionIndex;
    }

    private IEnumerator ShowConfirmationBox()
    {
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        confirmationPrompt.SetActive(false);
    }
}
