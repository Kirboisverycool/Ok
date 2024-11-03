using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyMachine : MonoBehaviour
{
    [SerializeField] public int goldAmount = 5;
    [SerializeField] public float generatingCooldown;
    [SerializeField] AudioClip moneySoundClip;

    bool isRunning = false;
    Bank bank;

    // Start is called before the first frame update
    void Start()
    {
        bank = FindObjectOfType<Bank>();
        StartCoroutine(StartIsRunning());
    }

    private IEnumerator StartIsRunning()
    {
        yield return new WaitForSeconds(generatingCooldown);
        isRunning = true;
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
            SFXManager.instance.PlaySFXClip(moneySoundClip, transform, 1f);
            yield return new WaitForSeconds(generatingCooldown);
            isRunning = true;
        }   
    }
}
