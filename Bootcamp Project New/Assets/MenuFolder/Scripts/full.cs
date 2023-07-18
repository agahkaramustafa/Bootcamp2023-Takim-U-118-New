using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class full : MonoBehaviour
{
    [SerializeField] private Toggle fullekrantusu;

    private void Start()
    {
        LoadFullScreen();
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void ApplyFullScreen()
    {
        PlayerPrefs.SetInt("masterFullscreen", fullekrantusu.isOn ? 1 : 0);
    }

    private void LoadFullScreen()
    {
        if (PlayerPrefs.HasKey("masterFullscreen"))
        {
            int localFullscreen = PlayerPrefs.GetInt("masterFullscreen");
            fullekrantusu.isOn = localFullscreen == 1;
            Screen.fullScreen = fullekrantusu.isOn;
        }
    }
}
