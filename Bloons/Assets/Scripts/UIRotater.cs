using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIRotater : MonoBehaviour
{
    [SerializeField] TextMeshPro myText;
    Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        myText.transform.rotation = camera.transform.rotation;
    }
}
