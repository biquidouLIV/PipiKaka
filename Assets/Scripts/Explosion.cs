using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Explosion : MonoBehaviour
{
    [SerializeField] private float strength =100f;
    List<GameObject> playerList = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerList.Add(other.gameObject);
            Debug.Log(playerList);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerList.Remove(other.gameObject);
            Debug.Log(playerList);
        }
    }

    public void Explode()
    {
        foreach (GameObject PLAYER in playerList)
        {
            
            Vector3 direction = (PLAYER.transform.position - transform.position).normalized;
            PLAYER.GetComponent<Rigidbody>().AddForce(direction * strength, ForceMode.Impulse);
            Debug.Log(direction*strength);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
