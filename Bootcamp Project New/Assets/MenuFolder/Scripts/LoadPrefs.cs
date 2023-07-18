using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadPrefs : MonoBehaviour
{
    [Header("Volume Setting")]
    [SerializeField] private TMP_Text s�f�rbes;
    [SerializeField] private Slider volumeSlider;

    private void Start()
    {
        LoadPlayerPrefs();
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
}
