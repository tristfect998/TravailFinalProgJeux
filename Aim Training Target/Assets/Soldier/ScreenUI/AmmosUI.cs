using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmosUI : MonoBehaviour {

    Text ammoLeftText;
    Text magazineSize;
    ShootMecanics gunMecanics;

	void Start () {
        gunMecanics = GetComponent<ShootMecanics>();

        ammoLeftText = this.transform.Find("BulletLeftText").GetComponent<Text>();
        magazineSize = this.transform.Find("MagazineBulletsText").GetComponent<Text>();

        magazineSize.text = gunMecanics.GetMagazineSize().ToString();
    }
	
	void Update () {
		
	}
}
