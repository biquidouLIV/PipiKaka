using UnityEngine;
using UnityEngine.InputSystem;

public class ArrowRotation : MonoBehaviour
{
    private Vector2 rotationInput;
    
    public void OnLook(InputAction.CallbackContext context)
    {
        rotationInput = context.ReadValue<Vector2>();
    }
    
    void FixedUpdate()
    {
        Vector3 dir = new (rotationInput.x, 0f, rotationInput.y);
        transform.rotation = Quaternion.Euler(dir);
    }

}