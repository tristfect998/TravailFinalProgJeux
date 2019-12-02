using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmesUIScript : MonoBehaviour {
    public Image AkImage;
    public Image M4Image;

    void Start()
    {
        AkImage.enabled = false;
        M4Image.enabled = false;
    }

    public void DisplayNewGunImage(Gun currentGun)
    {
        switch (currentGun)
        {
            case Gun.AK:
                AkImage.enabled = true;
                M4Image.enabled = false;
                break;
            case Gun.M4:
                AkImage.enabled = false;
                M4Image.enabled = true;
                break;
        }
    }

    public enum Gun
    {
        AK,
        M4
    }
}
