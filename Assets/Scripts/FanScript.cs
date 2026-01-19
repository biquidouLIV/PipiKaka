using UnityEngine;

public class FanScript : MonoBehaviour
{
    [SerializeField] private float puissance = 2000f;
    [SerializeField] private Vector3 directionPoussee = new Vector3(1, 0, 0);

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero; 
            rb.angularVelocity = Vector3.zero;
            Vector3 directionFinale = directionPoussee.normalized;
            rb.AddForce(directionFinale * puissance, ForceMode.Impulse);
        }
    }
}

