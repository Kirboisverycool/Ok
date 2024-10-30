using System;
using UnityEditor;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed;

    TargetLocator targetLocator;
    Transform target;

    private void Start()
    {
        targetLocator = GetComponentInParent<TargetLocator>();
    }

    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = projectileSpeed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    private void OnTriggerEnter(Collider collision)
    {
        EnemyHealth enemy = collision.GetComponent<EnemyHealth>();

        if(enemy)
        {
            enemy.TakeHit(targetLocator.damage, targetLocator.shieldDamage);
            Destroy(gameObject);
        }
    }
}
