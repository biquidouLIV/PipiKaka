using System;
using UnityEngine;

public class StickyBlock : MonoBehaviour
{
    
    private void OnCollisionEnter(Collision other)
    {
        other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll; // ça freeze les positions et rotations de l'objet 
    }

    private void OnCollisionExit(Collision other)
    {
        other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None; // ça unfreeze les positions et rotations
    }
}
