using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public bool isPlaceable;
    public bool hasTower;
    public bool isWaterTile;
    [SerializeField] List<Tower> towers = new List<Tower>();

    BuildTower buildTower;
    MeshRenderer renderer;

    public bool GetHasTower()
    {
        return hasTower;
    }

    public bool GetIsPlaceable()
    {
        return isPlaceable;
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(isPlaceable && buildTower.turretIndex != 10 && isWaterTile == false && buildTower.turretIndex != 3)
            {
                bool isPlacedTower = towers[buildTower.GetTurretIndex()].CreateTower(towers[buildTower.GetTurretIndex()], transform.position, gameObject);
                renderer.material.color = Color.white;
                isPlaceable = !isPlacedTower;
            }
            else if(isWaterTile && isPlaceable && buildTower.turretIndex == 3) 
            {
                bool isPlacedTower = towers[buildTower.GetTurretIndex()].CreateTower(towers[buildTower.GetTurretIndex()], transform.position, gameObject);
                renderer.material.color = Color.white;
                isPlaceable = !isPlacedTower;
            }
        }

        if (buildTower.turretIndex == 10)
        {
            renderer.material.color = Color.white;
        }
        if (isWaterTile && buildTower.turretIndex != 3)
        {
            renderer.material.color = Color.white;
        }

        if (buildTower.turretIndex != 10 && isWaterTile == false && buildTower.turretIndex != 3)
        {
            Highlight();
        }
        if(isWaterTile && buildTower.turretIndex == 3)
        {
            Highlight();
        }
    }

    private void Highlight()
    {
        if(isPlaceable)
        {
            renderer.material.color = Color.gray;
        }
    }

    private void OnMouseExit()
    {
        renderer.material.color = Color.white;
    }

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponentInChildren<MeshRenderer>();
        buildTower = FindObjectOfType<BuildTower>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
