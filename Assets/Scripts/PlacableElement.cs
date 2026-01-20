using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlacableElement : MonoBehaviour
{
    private bool isSelected;
    private Camera mainCamera;
    private Vector3 offset;
    private float zDepth = 0f;

    private void Start()
    {
        mainCamera = Camera.main;
        zDepth = transform.position.z;
    }

    private void Update()
    {

        if (isSelected && Input.GetKeyDown(KeyCode.R))
        {
            transform.Rotate(new Vector3(0, 0, -90));
        }


        if (isSelected)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = mainCamera.WorldToScreenPoint(transform.position).z;

            Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);
            transform.position = new Vector3(worldPos.x, worldPos.y, zDepth);
        }

        if (Input.GetMouseButtonDown(0))
        {
            HandleSelection();
        }
    }

    private void HandleSelection()
    {
        if (isSelected)
        {
            if (CanPlaceObject())
            {
                DropObject();
            }
            else
            {
                Debug.Log("prout");
            }
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == transform)
                {
                    PickupObject();
                }
            }
        }
    }

    private bool CanPlaceObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);

        foreach (var hit in hits)
        {
            if (hit.collider.CompareTag("Platform") && hit.transform != transform)
            {
                return false;
            }
        }

        return true;
    }

    private void PickupObject()
    {
        isSelected = true;
        gameObject.tag = "SelectedPlatform";
    }

    private void DropObject()
    {
        isSelected = false;
        gameObject.tag = "Platform";
        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), zDepth);
    }
}
