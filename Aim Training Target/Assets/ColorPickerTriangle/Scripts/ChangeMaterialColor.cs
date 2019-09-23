using UnityEngine;
using System.Collections;

public class ChangeMaterialColor : MonoBehaviour {

    private ColorPickerTriangle colorPicker;
    private Material material;

    void Start()
    {
        material = GetComponent<SkinnedMeshRenderer>().material;
        colorPicker = GameObject.FindObjectOfType<ColorPickerTriangle>();
        colorPicker.SetNewColor(material.color);
    }

    void Update()
    {
        material.color = colorPicker.TheColor;
    }
}
