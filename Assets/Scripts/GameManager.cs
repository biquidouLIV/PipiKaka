using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    ProgramState state = ProgramState.Setup;
    
    public enum ProgramState
    {
        Setup,
        Build,
        Play,
        GameOver
    }

    public void Update()
    {
        switch (state)
        {
            case ProgramState.Setup:
                Debug.Log("Phase de Setup");
                break;
            case ProgramState.Build:
                Debug.Log("Phase de Build");
                break;
            case ProgramState.Play:
                Debug.Log("Phase de Play");
                break;
            case ProgramState.GameOver:
                Debug.Log("Phase de GameOver");
                break;
        }

        if (Input.GetMouseButtonDown(0))
        {
            state = ProgramState.Build;
        }
    }
    
}
