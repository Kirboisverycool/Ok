using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int goldReward;
    [SerializeField] int goldPenalty;
    Bank bank;

    // Start is called before the first frame update
    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RewardGold()
    {
        bank.Deposit(goldReward);
    }

    public void StealGold()
    {
        bank.Withdraw(goldPenalty);
    }
}
