using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5;
    [SerializeField] int shieldSystem;

    WaveHandler waveHandler;
    Enemy enemy;
    [SerializeField] int currentHitPoints = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentHitPoints = maxHitPoints;
        enemy = GetComponent<Enemy>();
        waveHandler = GetComponentInParent<WaveHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void TakeHit(int damage, int shieldDamage)
    {
        if(shieldSystem <= shieldDamage)
        {
            currentHitPoints -= damage;
        }

        if (currentHitPoints <= 0)
        {
            Destroy(gameObject);
            waveHandler.waves[waveHandler.GetCurrentWaveIndex()].enemiesLeft--;
            enemy.RewardGold();
        }
    }
}
