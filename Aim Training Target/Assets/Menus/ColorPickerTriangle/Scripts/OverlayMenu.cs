using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;

public class OverlayMenu : MonoBehaviour {

    ChangeMaterialColor[] colors;

    public ColorsSettings colorsSettingsPrefab;

    void Start()
    {
        colors = FindObjectsOfType<ChangeMaterialColor>();

        LoadColors();
    }

    public void LoadBackOptionMenu()
    {
        SaveColors();
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
    }

    public void SaveColors()
    {
        if (FindObjectOfType<ColorsSettings>() != null)
        {
            ColorsSettings colorsSettings = FindObjectOfType<ColorsSettings>();

            SetColor(colorsSettings);
        }
        else
        {
            ColorsSettings colorsSettings = Instantiate(colorsSettingsPrefab);

            SetColor(colorsSettings);
        }
    }

    void LoadColors()
    {
        if (FindObjectOfType<ColorsSettings>() != null)
        {
            ColorsSettings colorsSettings = FindObjectOfType<ColorsSettings>();

            foreach (ChangeMaterialColor color in colors)
            {
                if (color.colorIndex == 0)
                {
                    color.SetColor(colorsSettings.PrimaryColor);
                }
                else if (color.colorIndex == 1)
                {
                    color.SetColor(colorsSettings.SecondaryColor);
                }
            }
        }
    }

    void SetColor(ColorsSettings _colorsSettings)
    {
        foreach (ChangeMaterialColor color in colors)
        {
            if (color.colorIndex == 0)
            {
                _colorsSettings.PrimaryColor = color.GetColor();
            }
            else if (color.colorIndex == 1)
            {
                _colorsSettings.SecondaryColor = color.GetColor();
            }
        }
    }
}