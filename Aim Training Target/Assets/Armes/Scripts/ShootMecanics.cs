using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMecanics : MonoBehaviour {

    public int gunDamage = 1;
    public float fireDelay = 0.1f;
    private float delayBeforeNextFire = 0;
    public float weaponRange = 50f;

    public int magazineSize = 30;
    private int bulletLeft;
    
    private AudioSource audioSource;
    public AudioClip CurrentGunShotSound;
    public AudioClip ReloadClip;

	void Start () {
        bulletLeft = magazineSize;
        audioSource = GetComponent<AudioSource>();
    }
	void Update () {
        if (bulletLeft > 0)
        {
            ProcessFire();
        }

        if (Input.GetAxis("Reload") != 0 && Input.GetAxis("Fire1") == 0 && bulletLeft != magazineSize)
        {
            audioSource.PlayOneShot(ReloadClip);
            bulletLeft = magazineSize;
        }
    }

    private void ProcessFire()
    {
        delayBeforeNextFire -= Time.deltaTime;

        if (Input.GetAxis("Fire1") != 0)
        {
            if (delayBeforeNextFire <= 0)
            {
                ShootBullet();
                audioSource.PlayOneShot(CurrentGunShotSound);
                bulletLeft -= 1;
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

    public int GetAmmoLeft()
    {
        return bulletLeft;
    }

    public int GetMagazineSize()
    {
        return magazineSize;
    }
}