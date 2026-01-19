using System;
using System.Collections;
using UnityEngine;

public class ABRythme : MonoBehaviour
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
}
