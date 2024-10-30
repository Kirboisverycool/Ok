using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.black;
    [SerializeField] Color blockedColor = Color.red;

    TextMeshPro label;
    Vector2Int cooridantes = new Vector2Int();
    Waypoint waypoint;

    private void Awake()
    {
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        waypoint = GetComponentInParent<Waypoint>();
        DisplayCoordinates();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.isPlaying == false)
        {
            DisplayCoordinates();
            UpdateObjectName();
        }

        ToggleLabels();
        ColorCoordinates();
    }

    private void ToggleLabels()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }

    private void ColorCoordinates()
    {
        if(waypoint.GetIsPlaceable() == true)
        {
            label.color = defaultColor;
        }    
        else
        {
            label.color = blockedColor;
        }    
    }

    private void DisplayCoordinates()
    {
        cooridantes.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        cooridantes.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);
        
        label.text = cooridantes.x + ", " + cooridantes.y;
    }

    private void UpdateObjectName()
    {
        transform.parent.name = cooridantes.ToString();
    }
}
