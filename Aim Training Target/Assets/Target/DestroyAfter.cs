using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour {

    public int timeToDestroy;

	void Start () {
        Destroy(gameObject, timeToDestroy);
	}
}
