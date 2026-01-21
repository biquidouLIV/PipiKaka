// Game Manager Script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    // GameState
    [Header("UI")] 
    [SerializeField] private Image[] playerScoreUI;

    
    [SerializeField] private GameObject[] Platforms;
    
    [SerializeField] private GameObject cameraLobby;
    [SerializeField] private GameObject mainCamera;
    public GameObject gameOverMenu;
    public int TotalPlayer;
    private int BuildPlayer;
    private int PlatformsPlaced;
    private int Round;
    private List<GameObject> deadPlayers = new ();
    private List<GameObject> scoreboard = new ();
    private int numberDeadPlayers;
    private Dictionary<GameObject, int> score = new ();
    private GameObject winner;
    [SerializeField] private int winScore = 10;

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
        Score,
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
                    
                    GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                    foreach (GameObject player in players)
                    {
                        score.TryAdd(player.gameObject, 0);
                    }
                    
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
                    BuildPlayer = 0;
                    //Debug.Log("phase Play");
                    TpPlayer();
                    deadPlayers.Clear();
                    scoreboard.Clear();
                    break;
                    
                case GameState.Score:
                    StartCoroutine(AfficherScore());
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
            if (Input.GetKeyDown(KeyCode.E))
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
        int placedCount = 0;

        foreach (var plat in platforms)
        {
            if (plat.isPlaced)
            {
                placedCount++;
            }
        }

        if (placedCount > PlatformsPlaced)
        {
            PlatformsPlaced = placedCount;
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

    public void PlayerDie(GameObject player)
    {
        deadPlayers.Add(player);
        CheckEndGame();
    }

    public void PlayerArrive(GameObject player)
    {
        scoreboard.Add(player);
        CheckEndGame();
    }

    private void CheckEndGame()
    {
        if (deadPlayers.Count + scoreboard.Count == TotalPlayer)
        {
            int i = 3;
            foreach (GameObject player in scoreboard)
            {
                score[player] += i;
                i--;
                
                if (score[player] >= winScore)
                {
                    winner = player;
                }
            }
            
            foreach (GameObject elem in deadPlayers)
            {
                scoreboard.Add(elem);
            }
            
            CurrentState = GameState.Score;
        }
    }
    
    private void SpawnRandomBlock()
    {
        for (int i = 0; i < 5; i++)
        {
            Instantiate(Platforms[UnityEngine.Random.Range(0, Platforms.Length)], new Vector3(-8 + 4 * i, -3, -1), Quaternion.identity);
        }
    }

    private IEnumerator AfficherScore()
    {
        int i = 0;
        foreach (var player in scoreboard)
        {
            playerScoreUI[i].fillAmount = (float)score[player]/winScore;
            i++;
        }
        
        
        
        yield return new WaitForSeconds(3f);
        
        if (winner != null)
        {
            CurrentState = GameState.GameOver;
        }
        else
        {
            CurrentState = GameState.Build;
        }
    }
}
