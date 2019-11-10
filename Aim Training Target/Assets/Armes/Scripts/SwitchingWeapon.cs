using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchingWeapon : MonoBehaviour {

    private WeaponDataBase controller;
	AudioSource audioSource;
	public AudioClip sound;
	
    void Start () {
        controller = FindObjectOfType<WeaponDataBase>();
        InstantiateWeapon(0);
		audioSource = GetComponent<AudioSource>();
    }

    void Update () {
        if (Input.GetAxis("Weapon1") == 1)
        {
            if (controller.currentWeaponId != 0)
            {
                DestroyCurrentHolding();
                InstantiateWeapon(0);
				audioSource.PlayOneShot(sound);
            }
        }
        else if (Input.GetAxis("Weapon2") == 1)
        {
            if (controller.currentWeaponId != 1)
            {
                DestroyCurrentHolding();
                InstantiateWeapon(1);
				audioSource.PlayOneShot(sound);				
            }
        }
    }

    void InstantiateWeapon(int idArme)
    {
        GameObject arme = controller.RecupererArmePrefab(idArme);
        Instantiate(arme, transform);
        controller.currentWeaponId = idArme;
    }

    void DestroyCurrentHolding()
    {
       GameObject currentHoldingWeapon = GameObject.FindWithTag("Weapon");
       Destroy(currentHoldingWeapon);
    }


    }
