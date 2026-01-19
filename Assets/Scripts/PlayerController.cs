using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject arrowParent;
    private Vector2 moveInput;
    private Vector2 rotationInput = new Vector2(0,1);
    private Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        rotationInput = context.ReadValue<Vector2>();
        Debug.Log(rotationInput);
    }
        
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            float tempsAppui = (float)context.duration;
            
        }
    }

    void FixedUpdate()
    {
        Vector3 dir = new (moveInput.x, 0f, 0f);
        rb.MovePosition(rb.position + dir * speed * Time.deltaTime);
        if (rotationInput.x != 0)
        {
            float angle = Mathf.Atan(rotationInput.y / rotationInput.x);
            if (rotationInput.x > 0)
                angle += Mathf.PI;
            Vector3 rot = new (0f, 0f, (angle+ Mathf.PI/2) * 180 / Mathf.PI);
        
            Debug.Log(rot);
            arrowParent.transform.rotation = Quaternion.Euler(rot);
        }
    }
    
}