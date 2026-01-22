// Sticky Block Script

using System.Collections;
using UnityEngine;

public class StickyBlock : MonoBehaviour
{

    private bool _canStick = true;
    
    private void OnCollisionEnter(Collision other)
    {
        if (!_canStick) 
            return;
        
        other.gameObject.GetComponent<Rigidbody>().constraints |=
            RigidbodyConstraints.FreezePositionX |
            RigidbodyConstraints.FreezePositionY; // freeze les positions et rotations de l'objet 
        StartCoroutine(WaitToUnstuck(other));
    }

    private IEnumerator WaitToUnstuck(Collision other)
    {
        yield return new WaitForSeconds(3f);
        other.gameObject.GetComponent<Rigidbody>().constraints &= ~(RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY);
        _canStick = false;
        yield return new WaitForSeconds(.5f);
        _canStick = true;
    }
    
    private void OnCollisionExit(Collision other)
    {
        other.gameObject.GetComponent<Rigidbody>().constraints &= ~(RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY); // unfreeze les positions et rotations au cas ou
    }
    
}

// END //