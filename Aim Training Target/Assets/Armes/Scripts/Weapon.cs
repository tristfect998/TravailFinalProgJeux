using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon {

    public int id;
    public string name;
    public GameObject weaponObject;
    public int magazineSize;
    public float fireDelay;
    public float weaponRange;
    public int gunDamage;
    public int bulletLeft;
    public AudioClip gunAudio;

}
