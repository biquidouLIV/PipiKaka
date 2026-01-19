using System;
using System.Collections;
using UnityEngine;

public class ABSpike : MonoBehaviour
{
    private BoxCollider colliderRythme;
    private void Start()
    {
        colliderRythme = GetComponent<BoxCollider>();
        StartCoroutine(Rythme());
    }

    private IEnumerator Rythme()
    {
        colliderRythme.isTrigger = true;
        yield return new WaitForSeconds(5);
        colliderRythme.isTrigger = false;
        yield return new WaitForSeconds(5);
        StartCoroutine(Rythme());
    }

    private void OnCollisionEnter(Collision other)
    {
        if (colliderRythme == false)
        {
            Destroy(other.gameObject);
        }
    }
}
