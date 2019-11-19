using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageManager : MonoBehaviour {
    public Image standingImage;
    public Image crouchingImage;
    public Image proningImage;

    void Start()
    {
        crouchingImage.enabled = false;
        proningImage.enabled = false;
    }

    public void DisplayPositionImage(Position currentPosition)
    {
        switch (currentPosition)
        {
            case Position.STANDING:
                standingImage.enabled = true;
                crouchingImage.enabled = false;
                proningImage.enabled = false;
                break;
            case Position.CROUCHING:
                standingImage.enabled = false;
                crouchingImage.enabled = true;
                proningImage.enabled = false;
                break;
            case Position.PRONING:
                standingImage.enabled = false;
                crouchingImage.enabled = false;
                proningImage.enabled = true;
                break;
        }
    }

    public enum Position
    {
        STANDING,
        CROUCHING,
        PRONING
    }
}
