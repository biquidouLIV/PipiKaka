using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlacableElement : MonoBehaviour
{
    private bool isSelected;
    private void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,Input.mousePosition.z+9));

        if (Input.GetMouseButtonDown(0))
        {
            if (isSelected)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            }
            
            
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x,Input.mousePosition.y,Input.mousePosition.z+10));
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if(hit.transform.position == transform.position)
                isSelected = !isSelected;
            }
        }
        if (isSelected)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                transform.Rotate(new Vector3(0,0,-90));
            }
            transform.position = mousePosition;
        }
    }
}
