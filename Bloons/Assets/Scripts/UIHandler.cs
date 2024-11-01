using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] List<Image> towerButtons = new List<Image>();

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
        FarmImage();
    }

    public void TurretButton()
    {
        if (towerButtons[0])
        {
            buildTower.TurretIndex = 0;
            TurretImage();
        }
    }
    public void SniperButton()
    {
        if (towerButtons[1])
        {
            buildTower.TurretIndex = 1;
            SniperImage();
        }
    }
    public void MachineGunButton()
    {
        if (towerButtons[2])
        {
            buildTower.TurretIndex = 2;
            MachineGunImage();
        }
    }
    public void FarmButton()
    {
        if (towerButtons[3])
        {
            buildTower.TurretIndex = 3;
            FarmImage();
        }
    }

    private void TurretImage()
    {
        if (buildTower.TurretIndex == 0)
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
        if (buildTower.TurretIndex == 1)
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
        if (buildTower.TurretIndex == 2)
        {
            towerButtons[2].color = Color.grey;
        }
        else
        {
            towerButtons[2].color = Color.white;
        }
    }

    private void FarmImage()
    {
        if (buildTower.TurretIndex == 3)
        {
            towerButtons[3].color = Color.grey;
        }
        else
        {
            towerButtons[3].color = Color.white;
        }
    }

}
