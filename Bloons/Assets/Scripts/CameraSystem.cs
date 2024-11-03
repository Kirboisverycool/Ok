using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;

public class CameraSystem : MonoBehaviour
{
    [Header("Zoom")]
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] float zoomSpeed = 10f;
    [SerializeField] float fieldOfViewMax = 50f;
    [SerializeField] float fieldOfViewMin = 10f;
    float targetFieldOfView = 50f;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float rotateSpeed = 300f;
    [SerializeField] int edgeScrollSize = 20;
    [SerializeField] bool isEdgeScrolling = false;
    private bool dragPanMoveActive;
    private Vector2 lastMousePosition;
    [SerializeField] float dragPanSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
        RotateCamera();
        DragPanMove();
        EdgeScrolling();
        ZoomCamera();
    }

    private void ZoomCamera()
    {
        if(Input.mouseScrollDelta.y > 0)
        {
            targetFieldOfView -= 5;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            targetFieldOfView += 5;
        }

        targetFieldOfView = Mathf.Clamp(targetFieldOfView, fieldOfViewMin, fieldOfViewMax);

        virtualCamera.m_Lens.FieldOfView = targetFieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, targetFieldOfView, Time.deltaTime * zoomSpeed);
    }

    private void DragPanMove()
    {
        Vector3 inputDir = new Vector3(0, 0, 0);

        if (Input.GetMouseButtonDown(1))
        {
            dragPanMoveActive = true;
            lastMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(1))
        {
            dragPanMoveActive = false;
        }

        if (dragPanMoveActive)
        {
            Vector2 mouseMovementDelta = (Vector2)Input.mousePosition - lastMousePosition;

            inputDir.x = mouseMovementDelta.x * dragPanSpeed;
            inputDir.z = mouseMovementDelta.y * dragPanSpeed;

            lastMousePosition = Input.mousePosition;
        }

        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;

        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    private void EdgeScrolling()
    {
        Vector3 inputDir = new Vector3(0, 0, 0);

        if (isEdgeScrolling)
        {
            if (Input.mousePosition.x < edgeScrollSize)
            {
                inputDir.x = -1f;
            }
            if (Input.mousePosition.y < edgeScrollSize)
            {
                inputDir.z = -1f;
            }
            if (Input.mousePosition.x > Screen.width - edgeScrollSize)
            {
                inputDir.x = +1f;
            }
            if (Input.mousePosition.y > Screen.height - edgeScrollSize)
            {
                inputDir.z = +1f;
            }
        }

        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;

        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    private void RotateCamera()
    {
        float rotateDir = 0f;

        if (Input.GetKey(KeyCode.Q))
        {
            rotateDir = +1f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotateDir = -1f;
        }

        transform.eulerAngles += new Vector3(0, rotateDir * rotateSpeed * Time.deltaTime, 0);
    }

    void MoveCamera()
    {
        Vector3 inputDir = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            inputDir.z = +1f;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            inputDir.z = -1f;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            inputDir.x = -1f;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            inputDir.x = +1f;
        }

        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;

        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
}
