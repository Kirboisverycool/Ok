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
    [SerializeField] List<Tower> towers = new List<Tower>();
    [SerializeField] AudioClip towerBuySoundClip;

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
            if(isPlaceable && buildTower.TurretIndex != 10)
            {
                bool isPlacedTower = towers[buildTower.GetTurretIndex()].CreateTower(towers[buildTower.GetTurretIndex()], transform.position, gameObject);
                SFXManager.instance.PlaySFXClip(towerBuySoundClip, transform, 1f);
                renderer.material.color = Color.white;
                hasTower = true;
                isPlaceable = !isPlacedTower;
            }
        }

        if (buildTower.TurretIndex == 10)
        {
            renderer.material.color = Color.white;
        }

        if (buildTower.TurretIndex != 10)
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
