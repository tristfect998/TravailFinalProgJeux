using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchingWeapon : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Weapon1") == 1)
        {
            WeaponDataBase weapon = FindObjectOfType<WeaponDataBase>();
            weapon.RecupererArme(1);
        }

        if (Input.GetAxis("Weapon2") == 1)
        {
            WeaponDataBase weapon = FindObjectOfType<WeaponDataBase>();
            weapon.RecupererArme(2);
        }
    }
}
