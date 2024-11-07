using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    [Header("Tower Cost")]
    [SerializeField] int cost = 5;
    [SerializeField] int sellGoldAmount;

    [Header("Upgrades")]
    [SerializeField] List<TextMeshProUGUI> towerUpgrades = new List<TextMeshProUGUI>();
    [SerializeField] Canvas towerCanvas;
    [SerializeField] int upgradeCost = 3;
    [SerializeField] int upgradeCostIncrease = 5;
    [SerializeField] int maxUpgrade = 10;
    [SerializeField] int currentUpgradeAmount;
    bool notOverObject = false;
    bool upgradeUIIsActive = false;
    public GameObject tilePlaced;

    [Header("Tower Upgrades")]
    [SerializeField] float damageUpgrade = 1;
    [SerializeField] float fireRateUpgrade = 0.2f;
    [SerializeField] float rangeUpgrade = 0.2f;
    [SerializeField] int shieldDamageUpgrade = 2;
    [SerializeField] Projectile projectile;

    [Header("Water Tower Upgrades")]
    [SerializeField] float slowDownAmountUpgrade = 0.5f;
    [SerializeField] float slowDownTimeUpgrade = 0.2f;

    [Header("Farm Upgrades")]
    [SerializeField] int goldRecievedUpgrade = 1;
    [SerializeField] float generatingCooldownUpgrade = 0.3f;

    [SerializeField] AudioClip towerBuySoundClip;

    MoneyMachine moneyMachine;
    Bank bank;
    Outline outline;
    TargetLocator targetLocator;
    Waypoint waypoint;
    BuildTower parent;

    public bool GetUpgradeUIIsActive()
    {
        return upgradeUIIsActive;
    }

    public bool CreateTower(Tower tower, Vector3 position, GameObject tile)
    {
        Bank bank = FindObjectOfType<Bank>();
        
        if(bank.GetCurrentBalance() >= cost)
        {
            var twr = Instantiate(tower.gameObject, position, Quaternion.identity);
            twr.GetComponent<Tower>().tilePlaced = tile;
            SFXManager.instance.PlaySFXClip(towerBuySoundClip, transform, 1f);
            bank.Withdraw(cost);
            return true;
        }

        return false;
    }

    private void OnMouseOver()
    {
        outline.enabled = true;
        notOverObject = true;

        if (Input.GetMouseButtonDown(0))
        {
            towerCanvas.gameObject.SetActive(true);
            upgradeUIIsActive = true;
        }
    }

    private void OnMouseExit()
    {
        notOverObject = false;

        if (upgradeUIIsActive)
        {
            return;
        }
        outline.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        moneyMachine = GetComponent<MoneyMachine>();
        bank = FindObjectOfType<Bank>();
        outline = FindObjectOfType<Outline>();
        targetLocator = GetComponent<TargetLocator>();
        parent = FindObjectOfType<BuildTower>();
        gameObject.transform.SetParent(parent.transform);
    }

    // Update is called once per frame
    void Update()
    {
        Upgrade();
        ShowStats();
        if (notOverObject == false)
        {
            if (upgradeUIIsActive && Input.GetMouseButtonDown(0))
            {
                towerCanvas.gameObject.SetActive(false);
                upgradeUIIsActive = false;
                outline.OutlineColor = Color.white;
                outline.enabled = false;
            }
        }
    }

    private void ShowStats()
    {
        if (gameObject.layer == 9)
        {
            if (upgradeUIIsActive)
            {
                towerUpgrades[0].text = "Damage: " + targetLocator.damage;
                towerUpgrades[1].text = "Fire Rate: " + targetLocator.GetFireRate();
                towerUpgrades[2].text = "Range: " + targetLocator.GetTowerRange();
                towerUpgrades[3].text = "Slow Down Amount " + targetLocator.slowDownAmount;
                towerUpgrades[4].text = "Slow Down Amount " + targetLocator.slowDownTime;
                if (currentUpgradeAmount < maxUpgrade)
                {
                    towerUpgrades[5].text = "Upgrade Cost: " + upgradeCost;
                }
                else
                {
                    towerUpgrades[5].text = "";
                }
            }
        }
        if (gameObject.layer == 8)
        {
            if(upgradeUIIsActive)
            {
                towerUpgrades[0].text = "Gold Recieved: " + moneyMachine.goldAmount;
                towerUpgrades[1].text = "Generating Cooldown: " + moneyMachine.generatingCooldown;
                if (currentUpgradeAmount < maxUpgrade)
                {
                    towerUpgrades[2].text = "Upgrade Cost: " + upgradeCost;
                }
                else
                {
                    towerUpgrades[2].text = "";
                }
            }
        }
        if(gameObject.layer == 7)
        {
            if (upgradeUIIsActive)
            {
                towerUpgrades[0].text = "Damage: " + targetLocator.damage;
                towerUpgrades[1].text = "Fire Rate: " + targetLocator.GetFireRate();
                towerUpgrades[2].text = "Range: " + targetLocator.GetTowerRange();
                if (currentUpgradeAmount < maxUpgrade)
                {
                    towerUpgrades[3].text = "Upgrade Cost: " + upgradeCost;
                }
                else
                {
                    towerUpgrades[3].text = "";
                }
            }
        }
    }

    private void Upgrade()
    {
        if (upgradeUIIsActive)
        {
            outline.OutlineColor = Color.cyan;
            if(currentUpgradeAmount < maxUpgrade && Input.GetKeyDown(KeyCode.Z) && upgradeCost <= bank.GetCurrentBalance())
            { 
                if(gameObject.layer == 7)
                {
                    bank.Withdraw(upgradeCost);
                    targetLocator.damage += damageUpgrade;
                    targetLocator.fireRate += fireRateUpgrade;
                    targetLocator.towerRange += rangeUpgrade;
                    upgradeCost += upgradeCostIncrease;
                    currentUpgradeAmount++;
                    if(currentUpgradeAmount == 5)
                    {
                        targetLocator.shieldDamage += shieldDamageUpgrade;
                    }
                }
                if(gameObject.layer == 8)
                {
                    bank.Withdraw(upgradeCost);
                    moneyMachine.goldAmount += goldRecievedUpgrade;
                    moneyMachine.generatingCooldown -= generatingCooldownUpgrade;
                    upgradeCost += upgradeCostIncrease;
                    currentUpgradeAmount++;
                }
                if (gameObject.layer == 9)
                {
                    bank.Withdraw(upgradeCost);
                    targetLocator.damage += damageUpgrade;
                    targetLocator.fireRate += fireRateUpgrade;
                    targetLocator.towerRange += rangeUpgrade;
                    targetLocator.slowDownTime += slowDownTimeUpgrade;
                    targetLocator.slowDownAmount += slowDownAmountUpgrade;
                    upgradeCost += upgradeCostIncrease;
                    currentUpgradeAmount++;
                    if (currentUpgradeAmount == 5)
                    {
                        targetLocator.shieldDamage += shieldDamageUpgrade;
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                upgradeUIIsActive = false;
                tilePlaced.GetComponent<Waypoint>().isPlaceable = true;
                bank.Deposit(sellGoldAmount);
                Destroy(gameObject);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                towerCanvas.gameObject.SetActive(false);
                upgradeUIIsActive = false;
                outline.OutlineColor = Color.white;
                outline.enabled = false;
            }
        }
    }
}
