using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ABRythme : MonoBehaviour
{
    [SerializeField] private TMP_Text indicateur;
    private BoxCollider colliderRythme;
    private void Start()
    {
        colliderRythme = GetComponent<BoxCollider>();
        StartCoroutine(Rythme());
    }

    private IEnumerator Rythme() 
    {
        indicateur.text = "A"; // change le texte qui apparait sur unity
        colliderRythme.isTrigger = true; // on met le collider en trigger pour que le joueur passe au travers
        yield return new WaitForSeconds(5); // on attend 5 sec
        indicateur.text = "B"; // change le texte qui apparait sur unity
        colliderRythme.isTrigger = false; // on remet le collider normal pour plus passer au travers
        yield return new WaitForSeconds(5);// on attend 5 sec
        StartCoroutine(Rythme());
    }
}

// faut faire le truc de quand le bloc se réactive lorsque le joueur est à l'intérieur ça tue le joueur mais là j'ai la flemme 
