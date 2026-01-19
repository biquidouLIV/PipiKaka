using System;
using UnityEngine;


public class RandomBlock : MonoBehaviour
{

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            int kk = UnityEngine.Random.Range(0,5);
        }
    }
}
