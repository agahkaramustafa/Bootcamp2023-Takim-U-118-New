using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    [Header("Volume Setting")]
    [SerializeField] private TMP_Text s�f�rbes;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private float defaultVolume = 1.0f;

    [Header("Graphics Settings")]

    [SerializeField] private TMP_Dropdown kalite;
    [SerializeField] private TMP_Dropdown cozunurluk;
    [SerializeField] private Toggle fullekrantusu;

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
        s�f�rbes.text = volume.ToString("0.0");
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
                kalite.value = QualitySettings.GetQualityLevel();
                cozunurluk.value = GetCurrentResolutionIndex();
                fullekrantusu.isOn = Screen.fullScreen;
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
            s�f�rbes.text = localVolume.ToString("0.0");
            volumeSlider.value = localVolume;
            AudioListener.volume = localVolume;
        }
    }

    private void LoadResolutions()
    {
        resolutions = Screen.resolutions;
        cozunurluk.ClearOptions();

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

        cozunurluk.AddOptions(options);
        cozunurluk.value = currentResolutionIndex;
        cozunurluk.RefreshShownValue();
    }

    private void LoadQualitySettings()
    {
        kalite.ClearOptions();

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

        kalite.AddOptions(options);
       kalite.value = currentQualityIndex;
        kalite.RefreshShownValue();

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
