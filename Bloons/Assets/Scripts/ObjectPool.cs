using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField][Range(0.1f, 30f)] float spawnTimer = 1f;
    [SerializeField][Range(1, 50)] int poolSize = 5;
    [SerializeField] float endTime;
    [SerializeField] float startTime;
    [SerializeField] int waveIncrease = 1;
    bool isSpawningEnemies = true;

    [SerializeField] GameObject[] pool;

    public GameObject[] GetPool()
    {
        return pool;
    }

    private void Awake()
    {
        PopulatePool();
    }

    private void PopulatePool()
    {
        pool = new GameObject[poolSize];
        
        for(int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemyPrefab, transform);
            pool[i].SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        endTime = poolSize;
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        SpawningWaveCooldown();
    }

    private void SpawningWaveCooldown()
    {
        if(isSpawningEnemies)
        {
            startTime += Time.deltaTime;
            if (startTime > endTime)
            {
                isSpawningEnemies = false;
                startTime = 0;
            }
        }
    }

    private IEnumerator SpawnEnemies()
    {
        while(startTime <= endTime)
        {
            EnableObjectPool();
            yield return new WaitForSeconds(spawnTimer);
        }
    }

    private void EnableObjectPool()
    {
        for(int i = 0; i < pool.Length; i++)
        {
            if (pool[i].activeInHierarchy == false)
            {
                pool[i].SetActive(true);
                return;
            }
        }
    }
}
