using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5;
    [SerializeField] int shieldSystem;
    [SerializeField] AudioClip damageSoundClip;
    public float enemySlowDownTime;
    [SerializeField] bool isSlowed = false;
    float maxSpeed;

    WaveHandler waveHandler;
    Enemy enemy;
    EnemyMover enemyMover;
    [SerializeField] int currentHitPoints = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentHitPoints = maxHitPoints;
        enemy = GetComponent<Enemy>();
        waveHandler = GetComponentInParent<WaveHandler>();
        enemyMover = GetComponent<EnemyMover>();

        maxSpeed = enemyMover.speed;
    }

    // Update is called once per frame
    void Update()
    {
        SlowDown();

        if (currentHitPoints <= 0)
        {
            Destroy(gameObject);
            waveHandler.waves[waveHandler.GetCurrentWaveIndex()].enemiesLeft--;
            enemy.RewardGold();
        }
    }

    private void SlowDown()
    {
        if(isSlowed) 
        {
            enemySlowDownTime -= Time.deltaTime;
        }

        if(enemySlowDownTime <= 0)
        {
            isSlowed = false;
            enemyMover.speed = maxSpeed;
            enemySlowDownTime = 0;
        }
    }

    /*private void OnParticleCollision(GameObject other)
    {
        currentHitPoints--;
        if (currentHitPoints <= 0)
        {
            Destroy(gameObject);
            waveHandler.waves[waveHandler.GetCurrentWaveIndex()].enemiesLeft--;
            enemy.RewardGold();
        }
    }*/

    public void TakeHit(int damage, int shieldDamage, float slowDown, float slowDownTime)
    {
        if(shieldSystem <= shieldDamage)
        {
            currentHitPoints -= damage;
            SFXManager.instance.PlaySFXClip(damageSoundClip, transform, 1f);
        }

        if(slowDown != 1 && isSlowed == false) 
        {
            enemySlowDownTime = 0;
            enemySlowDownTime = slowDownTime;
            enemyMover.speed /= slowDown;
            isSlowed = true;
        }
    }

    /*public void AoeHit(int aoeDamage, int aoeShieldDamage, float slowDown, float slowDownTime)
    {
        if (shieldSystem <= aoeShieldDamage)
        {
            currentHitPoints -= aoeDamage;
            SFXManager.instance.PlaySFXClip(damageSoundClip, transform, 1f);
        }
        if (slowDown != 1 && isSlowed == false)
        {
            enemySlowDownTime = 0;
            enemySlowDownTime = slowDownTime;
            enemyMover.speed /= slowDown;
            isSlowed = true;
        }
    }*/
}
