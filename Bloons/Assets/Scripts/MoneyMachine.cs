using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyMachine : MonoBehaviour
{
    [SerializeField] public int goldAmount = 5;
    [SerializeField] public float generatingCooldown;
    bool isRunning = true;
    Bank bank;

    // Start is called before the first frame update
    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(GenerateGold());
    }

    private IEnumerator GenerateGold()
    {
        while(isRunning)
        {
            bank.Deposit(goldAmount);
            isRunning = false;
            yield return new WaitForSeconds(generatingCooldown);
            isRunning = true;
        }   
    }
}
