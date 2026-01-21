// Game Manager Script

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    // GameState
    [SerializeField] private GameObject[] Platforms;
    
    [SerializeField] private GameObject cameraLobby;
    [SerializeField] private GameObject mainCamera;
    public GameObject gameOverMenu;
    public int TotalPlayer;
    private int BuildPlayer;
    private int PlatformsPlaced;
    private int Round;


    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }
    private void Start()
    {
        CurrentState = GameState.Setup;
    }
    public enum GameState
    {
        Setup,
        Build,
        Wait,
        Play,
        GameOver
    }
    
    
    private GameState _currentState;
    
    public GameState CurrentState
    {
        get => _currentState;
        set
        {
            _currentState = value;
            switch (_currentState)
            {
                case GameState.Setup:
                    cameraLobby.SetActive(true);
                    mainCamera.SetActive(false);
                    PlayerCount();
                    BuildPlayer = 0;
                    break;
                case GameState.Build:
                    if (TotalPlayer == 0)
                    {
                        PlayerCount();
                    } 
                    
                    if (BuildPlayer >= TotalPlayer)
                    {
                        CurrentState = GameState.Play;
                        return;
                    }
                    
                    cameraLobby.SetActive(false);
                    mainCamera.SetActive(true);
                    
                    SpawnRandomBlock();

                    CurrentState = GameState.Wait;
                    break;
                case GameState.Wait:
                    break;
                
                case GameState.Play:
                    Debug.Log("phase Play");
                    TpPlayer();
                    
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

    private void Update()
    {
        if (_currentState == GameState.Setup)
        {
            if (Input.GetMouseButtonDown(2))
            {
                PlayerCount();
                CurrentState = GameState.Build;
            }
        }
        if (CurrentState == GameState.Wait)
        {
            PlatformsCheck();
        }
    }

    private void PlatformsCheck()
    {
        PlacableElement[] platforms = FindObjectsOfType<PlacableElement>();
        int PlacedCount = 0;

        foreach (var plat in platforms)
        {
            if (plat.isPlaced)
            {
                PlacedCount++;
            }
        }

        if (PlacedCount > PlatformsPlaced)
        {
            PlatformsPlaced = PlacedCount;
            BuildPlayer++;
            CurrentState = GameState.Build;
        }
    }


    private void PlayerCount()
    {
        TotalPlayer = GameObject.FindGameObjectsWithTag("Player").Length;
    }
    private void TpPlayer()
    {
        int i = 0;
        foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
        {
            player.gameObject.transform.position = new Vector3(-16, 4 + i, 0);
            i++;
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
    


    private void SpawnRandomBlock()
    {
        for (int i = 0; i < 5; i++)
        {
            Instantiate(Platforms[UnityEngine.Random.Range(0, Platforms.Length)], new Vector3(-8 + 4 * i, -3, -1), Quaternion.identity);
        }
    }
}
