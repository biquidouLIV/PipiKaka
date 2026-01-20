using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlacableElement : MonoBehaviour
{
    private bool isSelected;
    private bool isPlaced;
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

            float gridX = Mathf.Round(worldPos.x);
            float gridY = Mathf.Round(worldPos.y);
            transform.position = new Vector3(gridX, gridY, zDepth);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Action();
        }
    }

    private void Action()
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
                    if (isPlaced)
                    {
                        return;
                    }
                    
                    PickupObject();
                    
                    DestroyOtherBlocks();
                }
            }
        }
    }

    private void DestroyOtherBlocks()
    {
        PlacableElement[] Blocks = FindObjectsOfType<PlacableElement>();
        foreach (var block in Blocks)
        {
            if (block == this)
            {
                continue;
            }

            if (block.isPlaced == false)
            {
                Destroy(block.gameObject);
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
        isPlaced = true;
        gameObject.tag = "Platform";
        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), 0);
    }
}
