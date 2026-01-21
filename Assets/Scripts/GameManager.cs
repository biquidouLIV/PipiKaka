// Game Manager Script

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    // GameState
    [SerializeField] private GameObject[] Platforms;
    
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
                    TpPlayer();
                    cameraLobby.SetActive(false);
                    mainCamera.SetActive(true);
                    SpawnRandomBlock();
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
                    Debug.Log("kaka");
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
                player.gameObject.transform.position = new Vector3(-16, 4 + i, 0);
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
            CurrentState = GameState.Build;
        }
    }

    private void SpawnRandomBlock()
    {
        for (int j =0; j < nbPlayerAlive; j++)
        {
            Debug.Log(j);
            for (int i = 0; i < 5; i++)
            {
                Instantiate(Platforms[UnityEngine.Random.Range(0, Platforms.Length-1)], new Vector3(-8+4*i,-3,-1), Quaternion.identity);
            }
        }

    }
}
