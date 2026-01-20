using System;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.position = new Vector3(-50,-1.5f,0);
            GameManager.Instance.nbPlayerAlive--;
        }

        if (GameManager.Instance.nbPlayerAlive <= 0)
        {
            GameManager.Instance.CurrentState = GameManager.GameState.Setup; // Faudra les mettres sur la phase de build quand elle sera faite
            //Debug.Log("Plus de joueur en vie");
        }
    }
}
