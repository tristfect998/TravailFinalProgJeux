﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchingWeapon : MonoBehaviour {

    private WeaponDataBase controller;
    AudioSource audioSource;
    public AudioClip sound;

    bool weaponInHand = false;
    bool weaponIsSwitching = false;

    public bool WeaponInHand {
        get { return weaponInHand; }
        set { weaponInHand = value; }
    }

    public UnityEvent WeaponHaveSwitched;

    void Start()
    {
        controller = GetComponentInChildren<WeaponDataBase>();
        audioSource = GetComponent<AudioSource>();

        if (WeaponHaveSwitched == null)
        {
            WeaponHaveSwitched = new UnityEvent();
        }

        InstantiateWeapon(0);
    }

    void Update()
    {
        if (!weaponIsSwitching && Input.GetAxis("Fire1") == 0 && Input.GetAxis("Aim") == 0)
        {
            if (Input.GetButtonDown("Weapon1"))
            {
                StartCoroutine(weaponSwitchingTime(0.5f, 0));
            }
            else if (Input.GetButtonDown("Weapon2"))
            {
                StartCoroutine(weaponSwitchingTime(0.5f, 1));
            }
        }
    }

    IEnumerator weaponSwitchingTime(float _time, int _id)
    {
        weaponIsSwitching = true;
        yield return new WaitForSeconds(_time);
        ProcessGunChanging(_id);
        weaponIsSwitching = false;
    }

    void ProcessGunChanging(int gunId)
    {
        if (controller.currentWeaponId != gunId)
        {
            DestroyCurrentHolding();
            InstantiateWeapon(gunId);
            audioSource.PlayOneShot(sound);
        }
    }

    void InstantiateWeapon(int idArme)
    {
        GameObject arme = controller.RecupererArmePrefab(idArme);
        Instantiate(arme, transform);

        WeaponInHand = true;
        controller.currentWeaponId = idArme;
        WeaponHaveSwitched.Invoke();
    }

    void DestroyCurrentHolding()
    {
        GameObject currentHoldingWeapon = GameObject.FindGameObjectWithTag("Weapon");
        WeaponInHand = false;
        Destroy(currentHoldingWeapon);
    }
}