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
            shootMecanics.ReloadGun.AddListener(ReloadGun);

            ProcessAmmoLeft();
        }
    }

    void ProcessAmmoLeft()
    {
        ammoLeftText.text = shootMecanics.GetBulletLeft().ToString();

        if (shootMecanics.GetBulletLeft() > 0 && shootMecanics.GetBulletLeft() <= CalculateLowAmmoNumber())
        {
            ammoLeftText.color = new Color32(255, 80, 80, 255);
        }
        else if (shootMecanics.GetBulletLeft() == 0)
        {
            ammoLeftText.color = new Color32(255, 10, 10, 255);
        }
        else
        {
            ammoLeftText.color = Color.white;
        }
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