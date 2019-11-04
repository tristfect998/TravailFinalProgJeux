using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, Damage {

    public GameObject explosion;
    public int lifeTotal = 1;
    
    private bool isDestroy = false;

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
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
            isDestroy = true;
        }
    }

    protected void SetLife(int _lifeTotal)
    {
        lifeTotal = _lifeTotal;
    }
}