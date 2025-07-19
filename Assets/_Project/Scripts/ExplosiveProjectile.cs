using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectile : MonoBehaviour
{
    [SerializeField] private int _damage = 20;
    [SerializeField] private float _radius = 6f;
    [SerializeField] private float _lifeTime = 5f;
    [SerializeField] private float _explosionForce = 7.5f;
    [SerializeField] private float _delayOnGround = 1f;
    [SerializeField] private LayerMask _groundLayer;

    private bool _exploded = false;

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_exploded) return;

        
        if ((_groundLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            Invoke(nameof(Explode), _delayOnGround);
        }
        else
        {
            Explode(); 
        }
    }

    private void Explode()
    {
        if (_exploded) return;
        _exploded = true;

        Collider[] hitables = Physics.OverlapSphere(transform.position, _radius);
        foreach (Collider hit in hitables)
        {
            var lifeController = hit.GetComponent<LifeController>();
            if (lifeController != null)
            {
                lifeController.TakeDamage(_damage);
            }

            var rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direction = (hit.transform.position - transform.position).normalized;
                rb.AddForce(direction * _explosionForce, ForceMode.Impulse);
            }
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
