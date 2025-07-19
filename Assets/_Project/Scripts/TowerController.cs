using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    private enum ProjectileType { Simple, Explosive }
    [SerializeField] private ProjectileType _type = ProjectileType.Simple;


    [SerializeField] private GameObject _simpleProjectilePrefab;
    [SerializeField] private GameObject _explosiveProjectilePrefab;
    [SerializeField] private float _attackRange = 10f;
    [SerializeField] private float _attackCooldown = 1f;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Transform _targetPlayer;

    private float _attackTimer = 1f;


    void Start()
    {
        
    }


    void Update()
    {
        if (_targetPlayer == null)
        {
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, _targetPlayer.position);

        if (distanceToPlayer <= _attackRange && _attackTimer <= 0f)
        {
            FireProjectile();
            _attackTimer = _attackCooldown;
        }

        _attackTimer -= Time.deltaTime;
    }

    private void FireProjectile()
    {
        Vector3 dir = (_targetPlayer.position - _firePoint.position).normalized;
        GameObject prefabToUse = null;

        switch (_type)
        {
            case ProjectileType.Simple:
                prefabToUse = _simpleProjectilePrefab;
                break;
            case ProjectileType.Explosive:
                prefabToUse = _explosiveProjectilePrefab;
                break;
        }

        if (prefabToUse == null) return;

        GameObject projectile = Instantiate(prefabToUse, _firePoint.position, Quaternion.LookRotation(dir));

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            float projectileSpeed = 15f;
            rb.velocity = dir * projectileSpeed;
        }
    }
}
