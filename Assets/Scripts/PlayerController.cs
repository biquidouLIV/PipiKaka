using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject arrowParent;
    private Vector2 moveInput;
    private Vector2 rotationInput = new Vector2(0,1);
    private Rigidbody rb;
    
    [SerializeField] private float forceMin = 5f;
    [SerializeField] private float forceMax = 12f;
    [SerializeField] private float tempsMax = 1f;
    [SerializeField] private bool appuiEnCours = false;
    [SerializeField] private float tempsAppui = 0f;
    [SerializeField] private bool auSol = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        if (appuiEnCours)
        {
            tempsAppui += Time.deltaTime;
        }
    }
    
    public void Jump(InputAction.CallbackContext ctx)
    {
        if (auSol && ctx.started)
        {
            appuiEnCours = true;
            auSol = false;
            tempsAppui = 0f;
        }
        else if (ctx.canceled)
        {
            if (appuiEnCours)
            {
                tempsAppui = Mathf.Clamp(tempsAppui, 0f, tempsMax);
                float multiplicateur = tempsAppui / tempsMax;
                rb.AddForce(new (rotationInput.x * 500 * multiplicateur, rotationInput.y * 500 * multiplicateur, 0f));
            }
            appuiEnCours = false;
            tempsAppui = 0f;
        }
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        rotationInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector3 dir = new (moveInput.x, 0f, 0f);
        rb.MovePosition(rb.position + speed * Time.deltaTime * dir);
        if (rotationInput.x != 0)
        {
            float angle = Mathf.Atan(rotationInput.y / rotationInput.x);

            Vector3 rot = new(0f, 0f, 0f);
            
            if (rotationInput.x > 0)
            {
                rot = new (0f, 0f, (angle + 3 * Mathf.PI/2) * 180 / Mathf.PI);
            }
            else
            {
                rot = new (0f, 0f, (angle + Mathf.PI/2) * 180 / Mathf.PI);
            }
        
            arrowParent.transform.rotation = Quaternion.Euler(rot);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            auSol = true;
        }
    }
}