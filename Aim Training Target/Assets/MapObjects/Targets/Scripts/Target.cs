using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, Damage {

    public GameObject explosion;
    public int lifeTotal = 1;
    public GameObject explosionlocation;
    GeneralScore generalScore;

    private bool isDestroy = false;

    void Start()
    {
        generalScore = FindObjectOfType<GeneralScore>();
    }

    public void TakeDamage(int damage)
    {
        lifeTotal -= damage;

        if (lifeTotal <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (!isDestroy)
        {
            Instantiate(explosion, new Vector3(explosionlocation.transform.position.x, explosionlocation.transform.position.y , explosionlocation.transform.position.z), Quaternion.identity);
            Destroy(gameObject);
            isDestroy = true;
            generalScore.AddScore();
        }
    }

    protected void SetLife(int _lifeTotal)
    {
        lifeTotal = _lifeTotal;
    }
}