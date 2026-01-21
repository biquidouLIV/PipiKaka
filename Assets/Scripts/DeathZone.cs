using System;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.position = new Vector3(-50,-1.28f,0);
            GameManager.Instance.PlayerDie(other.gameObject);
        }
    }
}
