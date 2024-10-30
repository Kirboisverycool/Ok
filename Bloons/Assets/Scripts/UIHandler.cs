using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Rendering;
using System;

public class UIHandler : MonoBehaviour
{
    [SerializeField] List<Image> towerImages = new List<Image>();

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

    private void TurretImage()
    {
        if (buildTower.TurretIndex == 0)
        {
            towerImages[0].color = Color.grey;
        }
        else
        {
            towerImages[0].color = Color.white;
        }
    }

    private void SniperImage()
    {
        if (buildTower.TurretIndex == 1)
        {
            towerImages[1].color = Color.grey;
        }
        else
        {
            towerImages[1].color = Color.white;
        }
    }

    private void MachineGunImage()
    {
        if (buildTower.TurretIndex == 2)
        {
            towerImages[2].color = Color.grey;
        }
        else
        {
            towerImages[2].color = Color.white;
        }
    }

    private void FarmImage()
    {
        if (buildTower.TurretIndex == 3)
        {
            towerImages[3].color = Color.grey;
        }
        else
        {
            towerImages[3].color = Color.white;
        }
    }
}
