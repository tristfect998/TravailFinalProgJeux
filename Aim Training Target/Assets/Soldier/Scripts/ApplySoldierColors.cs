using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplySoldierColors : MonoBehaviour {

    public int colorIndex = 0;
    Material material;

	void Start () {
        material = GetComponent<SkinnedMeshRenderer>().material;

        if (FindObjectOfType<ColorsSettings>() != null)
        {
            ColorsSettings colorsSettings = FindObjectOfType<ColorsSettings>();

            SetColor(colorsSettings);
        }
    }

    void SetColor(ColorsSettings _colorsSettings)
    {
        if (colorIndex == 0)
        {
            material.color = _colorsSettings.PrimaryColor;
        }
        else if (colorIndex == 1)
        {
            material.color = _colorsSettings.SecondaryColor;
        }
    }
}
