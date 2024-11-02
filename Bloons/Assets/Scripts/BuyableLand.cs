using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BuyableLand : MonoBehaviour
{
    [SerializeField] public bool isBuyableLand = true;
    [SerializeField] List<Waypoint> waypoints = new List<Waypoint>();
    [SerializeField] List<BuyableLand> buyableLands = new List<BuyableLand>();
    [SerializeField] int cost = 10;
    [SerializeField] int costIncrease = 5;
    [SerializeField] GameObject highlight;
    [SerializeField] TextMeshPro costText;
    [SerializeField] EnemyMover[] enemyMovers;
    [SerializeField] BuyableLand landLock;

    BoxCollider landCollider;
    AddPaths addPath;
    Bank bank;

    public bool GetIsBuyableLand()
    {
        return isBuyableLand;
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            BuyLand();
        }

        if(isBuyableLand == false)
        {
            return;
        }
        if(landLock != null)
        {
            if (landLock.GetIsBuyableLand() == true)
            {
                return;
            }
        }

        highlight.SetActive(true);
    }

    public void BuyLand()
    {
        if(landLock != null)
        {
            if (landLock.GetIsBuyableLand() == false)
            {
                if (isBuyableLand && bank.GetCurrentBalance() >= cost)
                {
                    foreach (Waypoint waypoints in waypoints)
                    {
                        waypoints.gameObject.SetActive(true);
                    }

                    if (buyableLands != null)
                    {
                        foreach (BuyableLand buyableLand in buyableLands)
                        {
                            buyableLand.gameObject.SetActive(true);
                        }
                    }

                    bank.Withdraw(cost);

                    landCollider.enabled = false;

                    isBuyableLand = false;

                    AddPath();
                }
            }
        }
        else
        {
            if (isBuyableLand && bank.GetCurrentBalance() >= cost)
            {
                foreach (Waypoint waypoints in waypoints)
                {
                    waypoints.gameObject.SetActive(true);
                }

                if (buyableLands != null)
                {
                    foreach (BuyableLand buyableLand in buyableLands)
                    {
                        buyableLand.gameObject.SetActive(true);
                    }
                }

                bank.Withdraw(cost);

                landCollider.enabled = false;

                isBuyableLand = false;

                AddPath();
            }
        }
    }

    private void AddPath()
    {
        for (int i = 0; i < enemyMovers.Length; i++)
        {
            if (waypoints[0].gameObject.CompareTag("Path Tiles"))
            {
                enemyMovers[i].GetPath().Add(waypoints[0]);
            }
            if (waypoints[1].gameObject.CompareTag("Path Tiles"))
            {
                enemyMovers[i].GetPath().Add(waypoints[1]);
            }
            if (waypoints[2].gameObject.CompareTag("Path Tiles"))
            {
                enemyMovers[i].GetPath().Add(waypoints[2]);
            }
            if (waypoints[3].gameObject.CompareTag("Path Tiles"))
            {
                enemyMovers[i].GetPath().Add(waypoints[3]);
            }
        }
    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        addPath = GetComponent<AddPaths>();
        landCollider = GetComponent<BoxCollider>();
        bank = FindObjectOfType<Bank>();
    }

    // Update is called once per frame
    void Update()
    {
        DisplayCost();
        enemyMovers = FindObjectsOfType<EnemyMover>();
    }

    private void DisplayCost()
    {
        costText.text = "Cost:" + cost.ToString();
    }
}
