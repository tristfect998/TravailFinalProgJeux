using UnityEngine;
using System.Collections;

public class ChangeMaterialColor : MonoBehaviour {

    private ColorPickerTriangle[] colorPickers;
    public int colorIndex;
    private Material material;

    void Start()
    {
        material = GetComponent<SkinnedMeshRenderer>().material;
        colorPickers = GameObject.FindObjectsOfType<ColorPickerTriangle>();

        if (colorPickers != null)
        {
            SetPickerColor(colorPickers[colorIndex], material.color);
        }
    }

    void Update()
    {
        if (colorPickers == null)
        {
            colorPickers = GameObject.FindObjectsOfType<ColorPickerTriangle>();
        }
        UpdateColors(colorPickers[colorIndex]);
    }

    void UpdateColors(ColorPickerTriangle colorPicker)
    {
        if (colorPicker.colorPickerIndex == 0)
        {
            material.color = colorPickers[0].TheColor;
        }
        else
        {
            material.color = colorPickers[1].TheColor;
        }
    }

    public Color GetColor()
    {
        return material.color;
    }

    public void SetColor(Color _color)
    {
        if (material == null)
        {
            material = GetComponent<SkinnedMeshRenderer>().material;
        }

        material.color = _color;

        if (colorPickers == null)
        {
            colorPickers = GameObject.FindObjectsOfType<ColorPickerTriangle>();
        }
        SetPickerColor(colorPickers[colorIndex], _color);
    }

    void SetPickerColor(ColorPickerTriangle colorPicker, Color _color)
    {
        if (colorPicker.colorPickerIndex == 0)
        {
            colorPickers[0].SetNewColor(_color);
        }
        else
        {
            colorPickers[1].SetNewColor(_color);
        }
    }
}
