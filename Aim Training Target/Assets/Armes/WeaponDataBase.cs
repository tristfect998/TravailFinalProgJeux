using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDataBase : MonoBehaviour {

    public List<Weapon> Item = new List<Weapon>();
    public int currentWeaponId = 0;

    public GameObject RecupererArmePrefab(int indice)
    {
        GameObject arme = null;
        if (indice < Item.Count)
        {
            arme = Item[indice].weaponObject;
        }

        return arme;
    }
}
