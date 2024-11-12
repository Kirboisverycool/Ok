using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeProjectile : MonoBehaviour
{
    [SerializeField] float aoeDistance = 2f;
    [SerializeField] int aoeDamage = 1;
    [SerializeField] int aoeShieldDamage = 1;
    [SerializeField] LayerMask enemyLayer;
    Projectile projectile;
    TargetLocator targetLocator;

    // Start is called before the first frame update
    void Start()
    {
        targetLocator = GetComponentInParent<TargetLocator>();
        projectile = GetComponent<Projectile>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AoeDamage()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, aoeDistance, enemyLayer);

        foreach (Collider collider in colliders)
        {
            if(collider.GetComponent<EnemyHealth>())
            {
                collider.GetComponent<EnemyHealth>().TakeHit(aoeDamage, targetLocator.slowDownAmount, targetLocator.slowDownTime);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, aoeDistance);
    }
}
