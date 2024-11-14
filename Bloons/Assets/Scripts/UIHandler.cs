using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class UIHandler : MonoBehaviour
{
    [SerializeField] List<Image> towerButtons = new List<Image>();
    [SerializeField] List<TextMeshProUGUI> infoTexts = new List<TextMeshProUGUI>();

    BuildTower buildTower;

    // Start is called before the first frame update
    void Start()
    {
        buildTower = FindObjectOfType<BuildTower>();
    }

    // Update is called once per frame
    void Update()
    {
        TurretImage();
        SniperImage();
        MachineGunImage();
        WaterTurretImage();
        FarmImage();
    }

    public void TurretEnterInfo()
    {
        infoTexts[0].gameObject.SetActive(true);
    }
    public void TurretExitInfo()
    {
        infoTexts[0].gameObject.SetActive(false);
    }
    public void SniperEnterInfo()
    {
        infoTexts[1].gameObject.SetActive(true);
    }
    public void SniperExitInfo()
    {
        infoTexts[1].gameObject.SetActive(false);
    }
    public void MachineGunEnterInfo()
    {
        infoTexts[2].gameObject.SetActive(true);
    }
    public void MachineGunExitInfo()
    {
        infoTexts[2].gameObject.SetActive(false);
    }
    public void WaterTurretEnterInfo()
    {
        infoTexts[3].gameObject.SetActive(true);
    }
    public void WaterTurretExitInfo()
    {
        infoTexts[3].gameObject.SetActive(false);
    }
    public void FarmEnterInfo()
    {
        infoTexts[4].gameObject.SetActive(true);
    }
    public void FarmExitInfo()
    {
        infoTexts[4].gameObject.SetActive(false);
    }

    public void TurretButton()
    {
        if (towerButtons[0])
        {
            buildTower.turretIndex = 0;
            TurretImage();
        }
    }
    public void SniperButton()
    {
        if (towerButtons[1])
        {
            buildTower.turretIndex = 1;
            SniperImage();
        }
    }
    public void MachineGunButton()
    {
        if (towerButtons[2])
        {
            buildTower.turretIndex = 2;
            MachineGunImage();
        }
    }
    public void WaterTurretButton()
    {
        if (towerButtons[3])
        {
            buildTower.turretIndex = 3;
            WaterTurretImage();
        }
    }
    public void FarmButton()
    {
        if (towerButtons[4])
        {
            buildTower.turretIndex = 4;
            FarmImage();
        }
    }

    private void TurretImage()
    {
        if (buildTower.turretIndex == 0)
        {
            towerButtons[0].color = Color.grey;
        }
        else
        {
            towerButtons[0].color = Color.white;
        }
    }

    private void SniperImage()
    {
        if (buildTower.turretIndex == 1)
        {
            towerButtons[1].color = Color.grey;
        }
        else
        {
            towerButtons[1].color = Color.white;
        }
    }

    private void MachineGunImage()
    {
        if (buildTower.turretIndex == 2)
        {
            towerButtons[2].color = Color.grey;
        }
        else
        {
            towerButtons[2].color = Color.white;
        }
    }
    private void WaterTurretImage()
    {
        if (buildTower.turretIndex == 3)
        {
            towerButtons[3].color = Color.grey;
        }
        else
        {
            towerButtons[3].color = Color.white;
        }
    }

    private void FarmImage()
    {
        if (buildTower.turretIndex == 4)
        {
            towerButtons[4].color = Color.grey;
        }
        else
        {
            towerButtons[4].color = Color.white;
        }
    }

}
