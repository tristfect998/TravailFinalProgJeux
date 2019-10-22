using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHit : MonoBehaviour {
    public GameObject Explosion;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Aim") == 1)
        {
            Instantiate(Explosion, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y +0.5f, gameObject.transform.position.z), Quaternion.identity);
            Destroy(gameObject);          
        }
    }
}
