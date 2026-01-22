using System;
using System.Collections;
using UnityEngine;

public class IceBlockScript : MonoBehaviour
{
    private Vector3 posInit;
    private void Start()
    {
        posInit = transform.position;
    }


    private IEnumerator OnCollisionEnter(Collision other)
    {
        transform.position = new Vector3(1000, 1000, 1000);
        yield return new WaitForSeconds(5);
        transform.position = posInit;
        SoundManager.instance.GlassBlock();
    }
    
}
