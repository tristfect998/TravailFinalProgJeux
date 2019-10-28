using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class ShootMecanics : MonoBehaviour {

    public int gunDamage = 1;
    public float fireDelay = 0.1f;
    private float delayBeforeNextFire = 0;
    public float weaponRange = 50f;

    public int magazineSize = 30;
    private int bulletLeft;

    public Transform gunEnd;
    public Camera fpsCamera;

    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    private AudioSource gunAudio;
    private LineRenderer laserLine;


	void Start () {
        laserLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        bulletLeft = magazineSize;

    }
	void Update () {
        if (bulletLeft > 0)
        {
            ProcessFire();
        }

        if (Input.GetAxis("Reload") != 0 && bulletLeft != magazineSize)
        {
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
                bulletLeft -= 1;
                delayBeforeNextFire = fireDelay;
            }
        }
    }

    private void ShootBullet()
    {
        StartCoroutine(ShootEffect());

        Vector3 rayOrigin = fpsCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        laserLine.SetPosition(0, gunEnd.position);

        if (Physics.Raycast(rayOrigin, fpsCamera.transform.forward, out hit, weaponRange))
        {
            laserLine.SetPosition(1, hit.point);

            if (hit.transform.tag == "Target")
            {
                TakeDamage(hit.transform.gameObject);
            }
        }
        else
        {
            laserLine.SetPosition(1, rayOrigin + (fpsCamera.transform.forward * weaponRange));
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

    private IEnumerator ShootEffect()
    {
        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }
}