using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using AimTrainingTarget.Soldier;

public class SwitchingWeapon : MonoBehaviour {

    private WeaponDataBase controller;
    AudioSource audioSource;
    public AudioClip sound;
    public GameObject AimGunSlot;
    public Vector3 AimingDifference;

    ArmesUIScript armesUI;
    bool AimGunSlotPositionChanged = false;
    bool weaponInHand = false;
    bool weaponIsSwitching = false;
    public FirstPersonController fpsc;

    public bool WeaponInHand {
        get { return weaponInHand; }
        set { weaponInHand = value; }
    }

    public UnityEvent WeaponHaveSwitched;

    void Start()
    {
        fpsc = GetComponentInParent<FirstPersonController>();
        armesUI = FindObjectOfType<ArmesUIScript>();
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
            if (WeaponInHand)
            {
                DestroyCurrentHolding();
            }

            audioSource.PlayOneShot(sound);
            InstantiateWeapon(gunId);
        }
    }

    void InstantiateWeapon(int idArme)
    {
        GameObject arme = controller.RecupererArmePrefab(idArme);
        Instantiate(arme, transform);
        WeaponInHand = true;
        controller.currentWeaponId = idArme;
        WeaponHaveSwitched.Invoke();
        if(AimGunSlot != null)
        {
            switch(idArme)
            {
                case 0: /*M4*/
                    fpsc.isM4(true);
                    if (AimGunSlotPositionChanged)
                    {
                        AimGunSlot.transform.localPosition -= AimingDifference;
                    }
                    armesUI.DisplayNewGunImage(ArmesUIScript.Gun.M4);
                    break;
                case 1: /*AK*/
                    fpsc.isM4(false);
                    AimGunSlot.transform.localPosition += AimingDifference;
                    AimGunSlotPositionChanged = true;
                    armesUI.DisplayNewGunImage(ArmesUIScript.Gun.AK);
                    break;
                default:
                    if (AimGunSlotPositionChanged)
                    {
                        AimGunSlot.transform.localPosition -= AimingDifference;
                    }
                    break;
            }
        }
    }

    void DestroyCurrentHolding()
    {
        GameObject currentHoldingWeapon = GameObject.FindGameObjectWithTag("Weapon");
        WeaponInHand = false;
        Destroy(currentHoldingWeapon);
    }
}