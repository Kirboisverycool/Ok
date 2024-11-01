using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveHandler : MonoBehaviour
{
    [SerializeField] float countdown;
    [SerializeField] GameObject spawnPoint;
    [SerializeField] TextMeshProUGUI nextWaveCountdownText;

    TextMeshPro waveText;

    public Wave[] waves;

    private int currentWaveIndex = 0;
    private bool readyToCountDown;

    public int GetCurrentWaveIndex()
    {
        return currentWaveIndex;
    }

    private void Awake()
    {
        UpdateCountdownText();
    }

    // Start is called before the first frame update
    void Start()
    {
        waveText = GetComponentInChildren<TextMeshPro>();

        readyToCountDown = true;

        for(int i = 0; i < waves.Length; i++)
        {
            waves[i].enemiesLeft = waves[i].enemies.Length;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(currentWaveIndex >= waves.Length)
        {
            return;
        }

        if(readyToCountDown)
        {
            countdown -= Time.deltaTime;
        }

        if (countdown <= 0)
        {
            readyToCountDown = false;

            countdown = waves[currentWaveIndex].timeToNextWave;
            StartCoroutine(SpawnWave());
        }

        if (waves[currentWaveIndex].enemiesLeft == 0)
        {
            readyToCountDown = true;
            currentWaveIndex++;
        }

        if(readyToCountDown)
        {
            nextWaveCountdownText.gameObject.SetActive(true);
        }
        else if(readyToCountDown == false)
        {
            nextWaveCountdownText.gameObject.SetActive(false);
        }

        waveText.text = "Wave: " + currentWaveIndex;
        nextWaveCountdownText.text = "Next Wave In: " + Mathf.RoundToInt(countdown);
    }

    private IEnumerator SpawnWave()
    {
        if(currentWaveIndex < waves.Length)
        {
            for (int i = 0; i < waves[currentWaveIndex].enemies.Length; i++)
            {
                Enemy enemy = Instantiate(waves[currentWaveIndex].enemies[i], spawnPoint.transform);

                enemy.transform.SetParent(spawnPoint.transform);

                yield return new WaitForSeconds(waves[currentWaveIndex].timeToNextEnemy);
            }
        }
    }

    private void UpdateCountdownText()
    {
        nextWaveCountdownText.text = "Next Wave In: " + waves[currentWaveIndex].timeToNextWave;
    }

    [System.Serializable]
    public class Wave
    {
        public Enemy[] enemies;
        public float timeToNextEnemy;
        public float timeToNextWave;

        [HideInInspector] public int enemiesLeft;
    }
}
