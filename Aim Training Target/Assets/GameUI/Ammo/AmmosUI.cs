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

    Weapon currentWeapon = null;

    int magSize = 0;

    void Start()
    {
        gunSlot = FindObjectOfType<SwitchingWeapon>();
        gunSlot.WeaponHaveSwitched.AddListener(WeaponSwitching);

        if (gunSlot != null)
        {
            weaponDataBase = gunSlot.GetComponentInChildren<WeaponDataBase>();
        }

        shootMecanics = gunSlot.GetComponentInChildren<ShootMecanics>();
        shootMecanics.BulletShot.AddListener(BulletShot);
        shootMecanics.ReloadGun.AddListener(ReloadGun);
    }

    void WeaponSwitching()
    {
        if (gunSlot.WeaponInHand)
        {
            currentWeapon = weaponDataBase.GetCurrentWeapon();

            magSize = currentWeapon.magazineSize;
            magazineSize.text = magSize.ToString();

            ProcessAmmoLeft();
        }
    }

    void ProcessAmmoLeft()
    {
        //ammoLeftText.text = currentWeapon.bulletLeft.ToString();

        /*if (currentWeapon.bulletLeft > 0 && currentWeapon.bulletLeft <= CalculateLowAmmoNumber())
        {
            ammoLeftText.color = new Color32(255, 80, 80, 255);
        }
        else if (currentWeapon.bulletLeft == 0)
        {
            ammoLeftText.color = new Color32(255, 10, 10, 255);
        }
        else
        {
            ammoLeftText.color = Color.white;
        }*/
    }

    void BulletShot()
    {
        ProcessAmmoLeft();
    }

    void ReloadGun()
    {
        ProcessAmmoLeft();
    }

    private int CalculateLowAmmoNumber()
    {
        return 10 * magSize / 100;
    }
}