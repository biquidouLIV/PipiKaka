using System;
using UnityEngine;

public class SpikeBlock : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.position = new Vector3(-50,-1.28f,0);
            GameManager.Instance.PlayerDie(other.gameObject);
        }
    }
}
