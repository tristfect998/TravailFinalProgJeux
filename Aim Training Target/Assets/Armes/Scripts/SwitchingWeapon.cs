using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchingWeapon : MonoBehaviour {

    private WeaponDataBase controller;
    AudioSource audioSource;
    public AudioClip sound;

    bool weaponInHand = false;

    public bool WeaponInHand {
        get { return weaponInHand; }
        set { weaponInHand = value; }
    }

    public UnityEvent WeaponHaveSwitched;

    void Start()
    {
        controller = GetComponentInChildren<WeaponDataBase>();
        InstantiateWeapon(0);
        audioSource = GetComponent<AudioSource>();

        if (WeaponHaveSwitched == null)
        {
            WeaponHaveSwitched = new UnityEvent();
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Weapon1"))
        {
            ProcessGunChanging(0);
        }
        else if (Input.GetButtonDown("Weapon2"))
        {
            ProcessGunChanging(1);
        }
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