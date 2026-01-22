// Ice Block Script

using System.Collections;
using UnityEngine;

public class IceBlockScript : MonoBehaviour
{
    
    private Vector3 _posInit;

    private IEnumerator OnCollisionEnter(Collision other)
    {
        _posInit = transform.position;
        transform.position = new Vector3(1000, 1000, 1000);
        yield return new WaitForSeconds(5);
        transform.position = _posInit;
        SoundManager.instance.GlassBlock();
    }
    
}

// END //