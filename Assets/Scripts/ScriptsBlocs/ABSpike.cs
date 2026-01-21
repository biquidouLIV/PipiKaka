using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class ABSpike : MonoBehaviour
{
    [SerializeField] private TMP_Text indicateur;
    private BoxCollider colliderRythme;
    private void Start()
    {
        colliderRythme = GetComponent<BoxCollider>();
        StartCoroutine(Rythme());
    }

    private IEnumerator Rythme() // c'est exactement la même chose que le scipt ABrythme
    {
        indicateur.text = "A";
        colliderRythme.isTrigger = true;
        yield return new WaitForSeconds(5);
        indicateur.text = "B";
        colliderRythme.isTrigger = false;
        yield return new WaitForSeconds(5);
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
