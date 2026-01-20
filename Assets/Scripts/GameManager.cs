// Game Manager Script

using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    // GameState
    [SerializeField] private GameObject cameraLobby;
    [SerializeField] private GameObject MainCamera;
    
    
    public enum GameState
    {
        Setup,
        Build,
        Play,
        GameOver
    }
    
    private GameState _currentState = GameState.Setup;
    
    public GameState currentState
    {
        get => _currentState;
        set
        {
            _currentState = value;
            switch (_currentState)
            {
                case GameState.Setup:
                    cameraLobby.SetActive(!cameraLobby.activeSelf);
                    Debug.Log("Phase de Setup");
                    break;
                case GameState.Build:
                    cameraLobby.SetActive(false);
                    MainCamera.SetActive(true);
                    Debug.Log("Phase de Build");
                    break;
                case GameState.Play:
                    Debug.Log("Phase de Play");
                    break;
                case GameState.GameOver:
                    Debug.Log("Phase de GameOver");
                    break;
                default:
                    Debug.Log("caca");
                    break;
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentState = GameState.Build;
        }
    }
}
