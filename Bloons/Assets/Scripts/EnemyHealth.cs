using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    [SerializeField] float smoothValue;
    [SerializeField] float maxHitPoints = 5;
    [SerializeField] AudioClip damageSoundClip;
    [SerializeField] Vector3 offset;
    Camera camera;
    [SerializeField] Transform target;
    public float enemySlowDownTime;
    float currentVelocity = 0f;
    [SerializeField] bool isSlowed = false;
    float maxSpeed;

    WaveHandler waveHandler;
    Enemy enemy;
    EnemyMover enemyMover;
    [SerializeField] float currentHitPoints = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentHitPoints = maxHitPoints;
        camera = FindObjectOfType<Camera>();
        enemy = GetComponent<Enemy>();
        waveHandler = GetComponentInParent<WaveHandler>();
        enemyMover = GetComponent<EnemyMover>();

        healthSlider.maxValue = maxHitPoints;
        healthSlider.value = currentHitPoints;

        maxSpeed = enemyMover.speed;
    }

    // Update is called once per frame
    void Update()
    {
        SlowDown();

        HealthSlider();
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

    void HealthSlider()
    {
        healthSlider.transform.rotation = camera.transform.rotation;
        healthSlider.transform.position = target.position + offset;

        float smoothSlider = Mathf.SmoothDamp(healthSlider.value, currentHitPoints, ref currentVelocity, smoothValue * Time.deltaTime);
        healthSlider.value = smoothSlider;
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

    public void TakeHit(float damage, float slowDown, float slowDownTime)
    {
        currentHitPoints -= damage;
        SFXManager.instance.PlaySFXClip(damageSoundClip, transform, 1f);

        if(slowDown != 1 && isSlowed == false) 
        {
            enemySlowDownTime = 0;
            enemySlowDownTime = slowDownTime;
            enemyMover.speed /= slowDown;
            isSlowed = true;
        }
        if (currentHitPoints <= 0)
        {
            Destroy(gameObject);
            waveHandler.waves[waveHandler.GetCurrentWaveIndex()].enemiesLeft--;
            enemy.RewardGold();
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
