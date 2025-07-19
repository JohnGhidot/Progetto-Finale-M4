using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleProjectile : MonoBehaviour
{
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _lifeTime = 5f;

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LifeController playerHealth = other.GetComponent<LifeController>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(_damage);
            }
            Destroy(gameObject);
        }
        else if (!other.isTrigger)
        {
            Destroy(gameObject);
        }
    }
}

