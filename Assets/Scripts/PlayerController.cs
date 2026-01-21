using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject arrowParent;
    [SerializeField] private GameObject explosion;
    [SerializeField] private Explosion explosionScript;
    private Vector2 moveInput;
    private Vector2 rotationInput = new Vector2(0,1);
    private Vector2 rotation = new Vector2(0,1);
    private Rigidbody rb;
    private float maxExplScale = 3.0f;
    
    [Header("Player")]
    [SerializeField] private float forceMin = 5f;
    [SerializeField] private float forceMax = 12f;
    [SerializeField] private float tempsMax = 1f;
    [SerializeField] private bool appuiEnCours = false;
    [SerializeField] private float tempsAppui = 0f;
    
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
        if (Physics.Raycast(transform.position, Vector3.down, 1f) && ctx.started)
        {
            appuiEnCours = true;
            tempsAppui = 0f;
        }
        else if (ctx.canceled)
        {
            explosionScript.Explode();
            if (appuiEnCours)
            {
                tempsAppui = Mathf.Clamp(tempsAppui, 0f, tempsMax);
                float multiplicateur = tempsAppui / tempsMax;
                rb.AddForce(new (rotation.x * 500 * multiplicateur, rotation.y * 500 * multiplicateur, 0f));
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
        if (rotationInput.x + rotationInput.y > 0.1 || rotationInput.x + rotationInput.y < -0.1)
        {
            rotation = rotationInput.normalized;
        }
    }

    private void FixedUpdate()
    {
        Vector3 dir = new (moveInput.x, 0f, 0f);
        rb.MovePosition(rb.position + speed * Time.deltaTime * dir);
        if (rotation.x != 0)
        {
            float angle = Mathf.Atan(rotation.y / rotation.x);

            Vector3 rotExpl = new(0f, 0f, 180f);
            Vector3 rot = new(0f, 0f, 0f);
            
            if (rotation.x > 0)
            {
                rot = new (0f, 0f, angle * 180 / Mathf.PI + 270);
            }
            else
            {
                rot = new (0f, 0f, angle * 180 / Mathf.PI + 90);
            }
            if (tempsAppui >= 0.2)
            {
                explosionScript.strength += 0.12f; //pour pas que la force soit trop grande
                explosionScript.strength = Mathf.Clamp(explosionScript.strength, 0f, 15f);
                Debug.Log(explosionScript.strength);
                
                float calculatedScale = MathF.Round(tempsAppui * 10) / 10;
                float finalScale = Mathf.Clamp(calculatedScale, 0f, maxExplScale);
                Vector3 explScale = new Vector3(finalScale, finalScale, finalScale);
                explosion.transform.localScale = explScale;

            }
            else
            {
                explosion.transform.localScale = new Vector3(1, 1, 1);
                explosionScript.strength = 0f;
            }
            arrowParent.transform.rotation = Quaternion.Euler(rot);
        }
    }
}