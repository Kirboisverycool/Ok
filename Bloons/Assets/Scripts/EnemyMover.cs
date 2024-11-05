using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] public float speed = 1f;
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] int enemyDamage = 10;
    [SerializeField] float yOffset = 2f;

    Health health;
    WaveHandler waveHandler;

    public List<Waypoint> GetPath()
    {
        return path;
    }

    private void Awake()
    {
        health = FindObjectOfType<Health>();
    }

    private void Start()
    {
        waveHandler = GetComponentInParent<WaveHandler>();
    }

    void OnEnable()
    {
        path.Clear();
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    private IEnumerator FollowPath()
    {
        for (int i = 1; i < path.Count; i++)
        {
            Waypoint waypoint = path[i];
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
            float travelPercent = 0f;

            transform.LookAt(endPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }

        FinishPath();
    }

    private void FinishPath()
    {
        health.DealDamage(enemyDamage);
        Destroy(gameObject);

        waveHandler.waves[waveHandler.GetCurrentWaveIndex()].enemiesLeft--;
    }

    private void FindPath()
    {
        GameObject parent = GameObject.FindGameObjectWithTag("Path");

        foreach(Transform child in parent.transform)
        {
            Waypoint waypoint = child.GetComponent<Waypoint>();

            if(waypoint != null && waypoint.gameObject.activeInHierarchy)
            {
                path.Add(waypoint);
            }
        }
    }

    private void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
