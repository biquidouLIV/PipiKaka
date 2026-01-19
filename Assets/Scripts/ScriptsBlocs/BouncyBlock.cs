using UnityEngine;

public class BouncyBlock : MonoBehaviour
{
    [SerializeField] float Force = 10f;

    private void OnCollisionEnter(Collision other) // c'est le bumper de mickael
    {
        Vector3 a = transform.position;
        Vector3 b = other.transform.position;
        Vector3 direction = (b - a).normalized;
        other.rigidbody.AddForce(direction * Force);
    }
}
