using UnityEngine;

public class RessortScript : MonoBehaviour
{
    [SerializeField] float Force = 200f;

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        Vector3 a = transform.position;
        Vector3 b = other.transform.position;
        Vector3 direction = (b - a).normalized;
        rb.AddForce(direction * Force);
    }
}
