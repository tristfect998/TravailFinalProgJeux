using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmosUI : MonoBehaviour {

    public Text ammoLeftText;
    public Text magazineSize;
    ShootMecanics gunMecanics;

    bool weaponInHand = false;

    void Start()
    {

    }

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Weapon") != null)
        {
            gunMecanics = GameObject.FindGameObjectWithTag("Weapon").GetComponent<ShootMecanics>();
            magazineSize.text = gunMecanics.GetMagazineSize().ToString();
            weaponInHand = true;
        }
        else
        {
            weaponInHand = false;
        }

        if (weaponInHand)
        {
            ammoLeftText.text = gunMecanics.GetAmmoLeft().ToString();

            if (gunMecanics.GetAmmoLeft() > 0 && gunMecanics.GetAmmoLeft() <= CalculateLowAmmoNumber())
            {
                ammoLeftText.color = new Color32(255, 80, 80, 255);
            }
            else if (gunMecanics.GetAmmoLeft() == 0)
            {
                ammoLeftText.color = new Color32(255, 10, 10, 255);
            }
            else
            {
                ammoLeftText.color = Color.white;
            }
        }
    }

    private int CalculateLowAmmoNumber()
    {
        return 10 * gunMecanics.GetMagazineSize() / 100;
    }
}
