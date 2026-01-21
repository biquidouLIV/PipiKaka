using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Explosion : MonoBehaviour
{
    [SerializeField] public float strength =0f;
    List<GameObject> playerList = new List<GameObject>();
    [SerializeField] private Animator explosion_animator;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerList.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerList.Remove(other.gameObject);
        }
    }

    public void Explode()
    {
        foreach (GameObject PLAYER in playerList)
        {
            Vector3 direction = (PLAYER.transform.position - transform.position).normalized;
            PLAYER.GetComponent<Rigidbody>().AddForce(direction * strength, ForceMode.Impulse);
        }
        
        explosion_animator.SetTrigger("Explode");
    }
}
