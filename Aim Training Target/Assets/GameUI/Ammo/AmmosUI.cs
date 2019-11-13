using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmosUI : MonoBehaviour {

    public Text ammoLeftText;
    public Text magazineSize;

    SwitchingWeapon gunSlot;
    WeaponDataBase weaponDataBase;
    ShootMecanics shootMecanics;

    int magSize = 0;

    void Start()
    {
        gunSlot = FindObjectOfType<SwitchingWeapon>();
        gunSlot.WeaponHaveSwitched.AddListener(WeaponSwitching);

        if (gunSlot != null)
        {
            weaponDataBase = gunSlot.GetComponentInChildren<WeaponDataBase>();
        }
    }

    void WeaponSwitching()
    {
        if (gunSlot.WeaponInHand)
        {
            Weapon currentWeapon = weaponDataBase.GetCurrentWeapon();
            magazineSize.text = currentWeapon.magazineSize.ToString();

            magSize = currentWeapon.magazineSize;

            shootMecanics = gunSlot.GetComponentInChildren<ShootMecanics>();
            shootMecanics.BulletShot.AddListener(BulletShot);
        }
    }

    void BulletShot()
    {
        ammoLeftText.text = shootMecanics.BulletLeft.ToString();

        if (shootMecanics.BulletLeft > 0 && shootMecanics.BulletLeft <= CalculateLowAmmoNumber())
        {
            ammoLeftText.color = new Color32(255, 80, 80, 255);
        }
        else if (shootMecanics.BulletLeft == 0)
        {
            ammoLeftText.color = new Color32(255, 10, 10, 255);
        }
        else
        {
            ammoLeftText.color = Color.white;
        }
    }

    private int CalculateLowAmmoNumber()
    {
        return 10 * magSize / 100;
    }
}