using UnityEngine;

public class BouncyBlock : MonoBehaviour
{
    [SerializeField] float strength = 1;
    
    private void OnCollisionEnter(Collision other)
    {
        Vector3 ballDirection = other.relativeVelocity;
        Vector3 normal = -other.contacts[0].normal;
        Vector3 direction = Vector3.Reflect(ballDirection, normal);
        
        other.rigidbody.AddForce(direction * strength);
    }
}
