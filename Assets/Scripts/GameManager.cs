// Game Manager Script

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    // GameState
    [SerializeField] private GameObject cameraLobby;
    [SerializeField] private GameObject mainCamera;
    public GameObject gameOverMenu;
    public int nbPlayerAlive;


    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    public enum GameState
    {
        Setup,
        Build,
        Play,
        GameOver
    }
    
    
    private GameState _currentState = GameState.Setup;
    
    public GameState CurrentState
    {
        get => _currentState;
        set
        {
            _currentState = value;
            switch (_currentState)
            {
                case GameState.Setup:
                    cameraLobby.SetActive(!cameraLobby.activeSelf);
                    //Debug.Log("Phase de Setup");
                    break;
                case GameState.Build:
                    //Debug.Log("Phase de Build");
                    break;
                case GameState.Play:
                    TpPlayer();
                    cameraLobby.SetActive(false);
                    mainCamera.SetActive(true);
                    //Debug.Log("Phase de Play");
                    break;
                case GameState.GameOver:
                    gameOverMenu.SetActive(!gameOverMenu.activeSelf);
                    //Debug.Log("Phase de GameOver");
                    break;
                default:
                    Debug.Log("caca");
                    break;
            }
        }
    }

    private void TpPlayer()
    {
        int i = 0;
        foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
        {
            nbPlayerAlive++;
            if (_currentState == GameState.Play)
            {
                player.gameObject.transform.position = new Vector3(-9, 4 + i, 0);
                i++;
            }
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            CurrentState = GameState.Play;
        }
    }
}
