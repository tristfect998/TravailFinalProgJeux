using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShootMecanics : MonoBehaviour {

    int magazineSize;
    float fireDelay;
    float weaponRange;
    int gunDamage;

    float delayBeforeNextFire = 0;

    SwitchingWeapon gunSlot;
    WeaponDataBase weaponDataBase;

    private AudioSource audioSource;
    public AudioClip CurrentGunShotSound;
    public AudioClip ReloadClip;

    public int BulletLeft { get; set; }

    public UnityEvent BulletShot;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        gunSlot = FindObjectOfType<SwitchingWeapon>();
        gunSlot.WeaponHaveSwitched.AddListener(WeaponSwitching);

        if (gunSlot != null)
        {
            weaponDataBase = gunSlot.GetComponentInChildren<WeaponDataBase>();
        }

        if (BulletShot == null)
        {
            BulletShot = new UnityEvent();
        }
    }

    void WeaponSwitching()
    {
        if (gunSlot.WeaponInHand)
        {
            Weapon currentWeapon = weaponDataBase.GetCurrentWeapon();

            magazineSize = currentWeapon.magazineSize;
            fireDelay = currentWeapon.fireDelay;
            weaponRange = currentWeapon.weaponRange;
            gunDamage = currentWeapon.gunDamage;

            BulletLeft = magazineSize;
        }
    }

    void Update()
    {
        Debug.Log(BulletLeft);
        if (BulletLeft > 0)
        {
            ProcessFire();
            Debug.Log("Test");
        }

        if (Input.GetButtonDown("Reload") && Input.GetButtonDown("Fire1") && BulletLeft != magazineSize)
        {
            audioSource.PlayOneShot(ReloadClip);
            BulletLeft = magazineSize;
        }
    }

    private void ProcessFire()
    {
        delayBeforeNextFire -= Time.deltaTime;

        if (Input.GetButtonDown("Fire1"))
        {
            if (delayBeforeNextFire <= 0)
            {
                ShootBullet();
                BulletShot.Invoke();
                audioSource.PlayOneShot(CurrentGunShotSound);
                BulletLeft -= 1;
                delayBeforeNextFire = fireDelay;
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