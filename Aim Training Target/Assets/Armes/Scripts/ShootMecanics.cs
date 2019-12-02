﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using AimTrainingTarget.Soldier;

public class ShootMecanics : MonoBehaviour {

    int magazineSize;
    float fireDelay;
    float weaponRange;
    int gunDamage;
    int bulletLeft;
    float reloadTime;

    float delayBeforeNextFire = 0;
    bool reloading = false;

    SwitchingWeapon gunSlot;
    WeaponDataBase weaponDataBase;

    Weapon currentWeapon = null;

    private AudioSource audioSource;
    AudioClip CurrentGunShotSound;
    public AudioClip ReloadClip;

    public UnityEvent BulletShot;
    public UnityEvent ReloadGun;

    public FirstPersonController fpsc;

    void Awake()
    {
        gunSlot = FindObjectOfType<SwitchingWeapon>();

        gunSlot.WeaponHaveSwitched.AddListener(WeaponSwitching);

        if (gunSlot != null)
        {
            weaponDataBase = gunSlot.GetComponentInChildren<WeaponDataBase>();
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        fpsc = GetComponentInParent<FirstPersonController>();
        if (BulletShot == null)
        {
            BulletShot = new UnityEvent();
        }
        if (ReloadGun == null)
        {
            ReloadGun = new UnityEvent();
        }
    }

    void WeaponSwitching()
    {
        if (gunSlot.WeaponInHand)
        {
            currentWeapon = weaponDataBase.GetCurrentWeapon();

            magazineSize = currentWeapon.magazineSize;
            fireDelay = currentWeapon.fireDelay;
            weaponRange = currentWeapon.weaponRange;
            gunDamage = currentWeapon.gunDamage;
            bulletLeft = currentWeapon.bulletLeft;
            CurrentGunShotSound = currentWeapon.gunAudio;
            reloadTime = currentWeapon.reloadTime;
        }
    }

    void Update()
    {
        if (bulletLeft > 0 && !reloading)
        {
            ProcessFire();
        }

        if (Input.GetButtonDown("Reload") && Input.GetAxis("Fire1") == 0 && bulletLeft != magazineSize)
        {
            StartCoroutine(GunReloadTime(reloadTime));
        }
    }

    private void ProcessFire()
    {
        delayBeforeNextFire -= Time.deltaTime;

        if (Input.GetAxis("Fire1") != 0)
        {
            if (delayBeforeNextFire <= 0)
            {
                ShootBullet();
                audioSource.PlayOneShot(CurrentGunShotSound);
                bulletLeft -= 1;
                currentWeapon.bulletLeft -= 1;
                delayBeforeNextFire = fireDelay;
                BulletShot.Invoke();
                fpsc.AddRecoil();
            }
        }
    }

    private void ShootBullet()
    {
        Vector3 rayOrigin = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, Camera.main.transform.forward, out hit, weaponRange))
        {
            if (hit.transform.tag == "Target")
            {
                TakeDamage(hit.transform.gameObject);
            }
        }
    }

    IEnumerator GunReloadTime(float _time)
    {
        audioSource.PlayOneShot(ReloadClip);
        reloading = true;
        yield return new WaitForSeconds(_time);
        bulletLeft = magazineSize;
        currentWeapon.bulletLeft = magazineSize;
        reloading = false;
        ReloadGun.Invoke();
    }

    private void TakeDamage(GameObject _gameObject)
    {
        if (_gameObject.GetComponent<Damage>() != null)
        {
            Damage damage = _gameObject.GetComponent<Damage>();

            if (damage != null)
            {
                damage.TakeDamage(gunDamage);
            }
        }
    }
}