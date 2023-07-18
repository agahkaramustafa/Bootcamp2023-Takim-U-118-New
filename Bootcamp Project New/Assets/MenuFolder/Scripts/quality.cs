using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class quality : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown kalite;

    private void Start()
    {
        LoadQualityLevel();
    }

    public void SetQualityLevel(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void ApplyQualityLevel()
    {
        int qualityIndex = kalite.value;
        PlayerPrefs.SetInt("masterQuality", qualityIndex);
    }

    private void LoadQualityLevel()
    {
        if (PlayerPrefs.HasKey("masterQuality"))
        {
            int localQuality = PlayerPrefs.GetInt("masterQuality");
            kalite.value = localQuality;
            QualitySettings.SetQualityLevel(localQuality);
        }
    }
}
