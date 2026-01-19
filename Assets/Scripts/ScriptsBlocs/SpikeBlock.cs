using System;
using UnityEngine;

public class SpikeBlock : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);
    }
}
