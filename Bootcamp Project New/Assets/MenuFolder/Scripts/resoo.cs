using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class resoo: MonoBehaviour
{
    [SerializeField] private TMP_Dropdown cozunurluk;
    private Resolution[] resolutions;

    private void Start()
    {
        LoadResolutions();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
    }

    public void ApplyResolution()
    {
        int resolutionIndex = cozunurluk.value;
        PlayerPrefs.SetInt("masterResolution", resolutionIndex);
        SetResolution(resolutionIndex);
    }

    private void LoadResolutions()
    {
        resolutions = Screen.resolutions;
        cozunurluk.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        int savedResolutionIndex = PlayerPrefs.GetInt("masterResolution");
        if (savedResolutionIndex >= resolutions.Length)
        {
            savedResolutionIndex = resolutions.Length - 1;
        }

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (i == savedResolutionIndex)
            {
                currentResolutionIndex = i;
            }
        }
        cozunurluk.AddOptions(options);
        cozunurluk.value = currentResolutionIndex;
        cozunurluk.RefreshShownValue();
    }
}
