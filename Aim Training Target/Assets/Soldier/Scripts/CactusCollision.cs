using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusCollision : MonoBehaviour {

    // Use this for initialization
    bool enterCollider = false;
    public Texture texture;
    public AudioClip audioClip;
    private AudioSource audioSource;
    private float delayBeforeSound;
    public float timeBeforeSound;

	void Start () {
        audioSource = GetComponent<AudioSource>();
        delayBeforeSound = timeBeforeSound;
	}


    private void OnTriggerEnter(Collider collisionObject)
    {
        if (collisionObject.tag == "Cactus")
        {
            delayBeforeSound = 0;
            enterCollider = true; 
        }
    }

    private void OnTriggerStay(Collider collisionObject)
    {
        if (collisionObject.tag == "Cactus")
        {
            delayBeforeSound -= Time.deltaTime;
            if (delayBeforeSound <= 0)
            {
                delayBeforeSound = timeBeforeSound;
                audioSource.PlayOneShot(audioClip);
            }
               
        }
    }

    private void OnTriggerExit(Collider collisionObject)
    {
        if (collisionObject.tag == "Cactus")
        {
            enterCollider = false;
        }
    }

    private void OnGUI()
    {
        if (enterCollider == true)
        {
            Debug.Log("GUI");
            GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), texture);
        }
    }
}
