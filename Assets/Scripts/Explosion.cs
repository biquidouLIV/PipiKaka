// Explosion Script

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class Explosion : MonoBehaviour
{
    
    [Header("Explosion Settings")]
    [SerializeField] public float strength = 0f;
    [FormerlySerializedAs("explosion_animator")] [SerializeField] private Animator explosionAnimator;
    
    private List<GameObject> _playerList = new();
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerList.Add(other.gameObject);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerList.Remove(other.gameObject);
        }
    }

    public void Explode()
    {
        foreach (GameObject player in _playerList)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            player.GetComponent<Rigidbody>().constraints &= ~(RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY);
            player.GetComponent<Rigidbody>().AddForce(direction * strength, ForceMode.Impulse);
        }
        
        explosionAnimator.SetTrigger("Explode");
        SoundManager.instance.ExplosionSound();
    }
    
}

// END //