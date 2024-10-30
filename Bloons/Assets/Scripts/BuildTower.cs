using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTower : MonoBehaviour
{
    public int TurretIndex = 0;

    public int GetTurretIndex()
    {
        return TurretIndex;
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
            TurretIndex = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TurretIndex = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            TurretIndex = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            TurretIndex = 3;
        }
    }
}
