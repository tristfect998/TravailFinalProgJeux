using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchingWeapon : MonoBehaviour {

    private int currentWeaponId = 0;
	// Use this for initialization
	void Start () {
        InstantiateWeapon(0);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetAxis("Weapon1") == 1)
        {
            if (currentWeaponId != 0)
            {
                DestroyCurrentHolding();
                InstantiateWeapon(0);
            }
        }

        if (Input.GetAxis("Weapon2") == 1)
        {
            if (currentWeaponId != 1)
            {
                DestroyCurrentHolding();
                InstantiateWeapon(1);
            }
        }
    }

    void InstantiateWeapon(int idArme)
    {
        WeaponDataBase weaponDataBase = FindObjectOfType<WeaponDataBase>();
        GameObject arme = weaponDataBase.RecupererArmePrefab(idArme);
        Instantiate(arme, transform);
        currentWeaponId = idArme;
    }

    void DestroyCurrentHolding()
    {
       /* GameObject currentHoldingWeapon = GetComponentInParent<GameObject>();
        Destroy(currentHoldingWeapon); */
    }


    }
