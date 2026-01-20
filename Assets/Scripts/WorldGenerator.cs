// Block Placement Script

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

//

public class WorldGenerator : MonoBehaviour
{
    
    // Game Variables
    
    [Header("Blocks")]
    [SerializeField] private List<GameObject> blockList;
    [SerializeField] private int maxBlocks;
    [SerializeField] private int minBlocks;
    [Space(10f)]
    
    //
    
    [Header("Limits")]
    [SerializeField] private int topCoord;
    [SerializeField] private int rightCoord;
    [SerializeField] private int bottomCoord;
    [SerializeField] private int leftCoord;
    
    // Private Variables

    private GameObject[] _blockArray;
    
    //
    
    // Base Functions
    
    private void Start()
    {
        CreateBlock();
    }
    
    //
    
    // Block Placing Functions

    private void GetRandomBlocks(int blocksNumber)
    {
        _blockArray = new GameObject[blocksNumber];
        for (int i = 0; i < blocksNumber; i++)
        {
            _blockArray[i] = blockList[Random.Range(0, blockList.Count)];
        }
    }
    
    //
 
    private void CreateBlock()
    {
        
        GetRandomBlocks(Random.Range(minBlocks,maxBlocks));
        List<(int, int)> posList = new List<(int, int)>();
        foreach (var elem in _blockArray)
        {
            int xCoord; int yCoord;
            
            while (true)
            {
                bool isGood = false;
                xCoord = Random.Range(leftCoord+1,rightCoord);
                yCoord = Random.Range(bottomCoord+1,topCoord);
                
                if (posList.Count == 0)
                {
                    break;
                }
                
                foreach (var element in posList)
                {
                    if (element.Item1 - 2 <= xCoord && xCoord <= element.Item1 + 2 && element.Item2 - 2 <= yCoord && yCoord <= element.Item2 + 2)
                    {
                        isGood = true;
                    }
                }

                if (isGood) continue;
                break;
            }
            
            posList.Add((xCoord, yCoord));
            
            Vector3 pos = new Vector3(xCoord, yCoord, 0);
            Quaternion rot = Quaternion.Euler(new Vector3(0, 0, 90 * Random.Range(0,4)));
            
            Instantiate(elem, pos, rot);
        }
    }
    
}

// END //
