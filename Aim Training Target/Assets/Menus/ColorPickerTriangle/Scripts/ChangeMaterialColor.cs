using UnityEngine;
using System.Collections;

public class ChangeMaterialColor : MonoBehaviour {

    private ColorPickerTriangle[] colorPickers;
    public int colorIndex = 0;
    private Material material;

    void Start()
    {
        material = GetComponent<SkinnedMeshRenderer>().material;
        colorPickers = GameObject.FindObjectsOfType<ColorPickerTriangle>();

        colorPickers[colorIndex].SetNewColor(material.color);
    }

    void Update()
    {
        material.color = colorPickers[colorIndex].TheColor;
    }
}
