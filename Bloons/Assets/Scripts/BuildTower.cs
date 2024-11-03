using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTower : MonoBehaviour
{
    public int turretIndex = 0;

    public int GetTurretIndex()
    {
        return turretIndex;
    }

    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        GetWhichTurretToBuild(); 
    }

    private void GetWhichTurretToBuild()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            turretIndex = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            turretIndex = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            turretIndex = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            turretIndex = 3;
        }
        if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            turretIndex = 4;
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            turretIndex = 10;
        }
    }
}
