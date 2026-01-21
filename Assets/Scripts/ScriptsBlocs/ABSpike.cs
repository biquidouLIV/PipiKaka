using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class ABSpike : MonoBehaviour
{
    [SerializeField] GameObject sprite;
    [SerializeField] private float time = 2.0f;
    private BoxCollider colliderRythme;
    private void Start()
    {
        colliderRythme = GetComponent<BoxCollider>();
        StartCoroutine(Rythme());
    }

    private IEnumerator Rythme() // c'est exactement la même chose que le scipt ABrythme
    {
        sprite.SetActive(false);
        colliderRythme.isTrigger = true;
        yield return new WaitForSeconds(time);
        sprite.SetActive(true);
        colliderRythme.isTrigger = false;
        yield return new WaitForSeconds(time);
        StartCoroutine(Rythme());
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.position = new Vector3(-50,-1.28f,0);
            GameManager.Instance.PlayerDie(other.gameObject);
        }
    }
}
