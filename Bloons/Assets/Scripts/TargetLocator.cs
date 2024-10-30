using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    float distance;
    [SerializeField] Transform weapon;
    Transform target;
    [SerializeField] ParticleSystem particle;

    [Header("Shooting")]
    [SerializeField] public float fireRate = 1f;
    [SerializeField] public int damage;
    [SerializeField] public int shieldDamage = 1;
    [SerializeField] public float towerRange;
    [SerializeField] Transform firePoint;
    [SerializeField] Transform parent;
    [SerializeField] GameObject projectilePrefab;
    private float fireCountdown = 0f;

    public GameObject GetProjectilePrefab()
    {
        return projectilePrefab;
    }

    public float GetFireRate()
    {
        return fireRate;
    }

    public float GetTowerRange()
    {
        return towerRange;
    }

    // Start is called before the first frame update
    void Start()
    {
        // find the target

        if(target == null)
        {
            return;
        }
        target = FindObjectOfType<EnemyMover>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }

    private void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach(Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if(targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            } 
        }

        distance = maxDistance;
        target = closestTarget;
    }

    private void AimWeapon()
    {
        //Attack(distance <= towerRange);
        weapon.LookAt(target);

        if(fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    private void Shoot()
    {
        if(distance <= towerRange)
        {
            GameObject projectileGO = (GameObject)Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            projectileGO.transform.SetParent(parent.transform);

            Projectile projectile = projectileGO.GetComponent<Projectile>();

            if (projectile != null)
            {
                projectile.Seek(target);
            }
        }
    }

    private void Attack(bool isActive)
    {
        var emmision = particle.emission;
        emmision.enabled = isActive;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, towerRange);
    }
}