using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMecanics : MonoBehaviour {

    public int gunDamage = 1;
    public float fireDelay = 0.1f;
    private float delayBeforeNextFire = 0;
    public float weaponRange = 50f;
    public Transform gunEnd;
    public Camera fpsCamera;

    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    private AudioSource gunAudio;
    private LineRenderer laserLine;


	void Start () {
        laserLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
	}
	void Update () {
        ProcessFire();
    }

    private void ProcessFire()
    {
        delayBeforeNextFire -= Time.deltaTime;

        if (Input.GetAxis("Fire1") != 0)
        {
            if (delayBeforeNextFire <= 0)
            {
                ShootBullet();

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

    private IEnumerator ShootEffect()
    {
        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }

    //https://learn.unity.com/tutorial/let-s-try-shooting-with-raycasts#
}