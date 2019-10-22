using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDataBase : MonoBehaviour {

    public List<Weapon> Item = new List<Weapon>();

    public GameObject RecupererArme(int indice)
    {
        GameObject arme = null;
        if (indice < Item.Count)
        {
            arme = Item[indice].WeaponObject;
        }

        return arme;
    }
}
