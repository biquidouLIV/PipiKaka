using System;
using UnityEngine;


public class RandomBlock : MonoBehaviour
{
    [SerializeField] private GameObject[] Platforms;
    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            Instantiate(Platforms[UnityEngine.Random.Range(0, Platforms.Length-1)], new Vector3(-8+4*i,-3,-1), Quaternion.identity);
        }
    }
}
