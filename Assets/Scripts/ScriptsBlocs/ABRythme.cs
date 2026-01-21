using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ABRythme : MonoBehaviour
{
    [SerializeField] private GameObject sprite_on;
    [SerializeField] private GameObject sprite_off;
    [SerializeField] private float time = 2.0f;
    private BoxCollider colliderRythme;
    private void Start()
    {
        colliderRythme = GetComponent<BoxCollider>();
        StartCoroutine(Rythme());
    }

    private IEnumerator Rythme() // c'est exactement la même chose que le scipt ABrythme
    {
        sprite_on.SetActive(false);
        sprite_off.SetActive(true);
        colliderRythme.isTrigger = true;
        
        yield return new WaitForSeconds(time);
        
        sprite_on.SetActive(true);
        sprite_off.SetActive(false);
        colliderRythme.isTrigger = false;
        
        yield return new WaitForSeconds(time);
        StartCoroutine(Rythme());
    }
}

// faut faire le truc de quand le bloc se réactive lorsque le joueur est à l'intérieur ça tue le joueur mais là j'ai la flemme 
