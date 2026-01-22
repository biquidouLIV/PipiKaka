// Game Manager Script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    // GameState
    [Header("UI")] 
    [SerializeField] private Image[] playerScoreUI;

    [SerializeField] private GameObject scoreUI;

    [SerializeField] private GameObject fondBlanc;

    
    [SerializeField] private GameObject[] Platforms;
    
    [SerializeField] private GameObject cameraLobby;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject LobbyUI;
    public GameObject gameOverMenu;
    public int TotalPlayer;
    private int BuildPlayer;
    private int PlatformsPlaced;
    private int Round;
    private List<GameObject> deadPlayers = new ();
    private List<GameObject> scoreboard = new ();
    private int numberDeadPlayers;
    private Dictionary<GameObject, int> score = new ();
    public Dictionary<GameObject, int> numPlayer = new ();
    private GameObject winner;
    [SerializeField] private int winScore = 10;
    [Header("Settings Multijoueur")]
    [SerializeField] private Color[] playerColors;
    

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
        scoreUI.SetActive(false);
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
                    fondBlanc.SetActive(true);
                    
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
                    SoundManager.instance.StartingSound();
                    fondBlanc.SetActive(false);
                    BuildPlayer = 0;
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

        if (CurrentState == GameState.Wait)
        {
            PlatformsCheck();
        }
    }

    public void StartGame()
    {
        PlayerCount();
        CurrentState = GameState.Build;
        LobbyUI.SetActive(false);
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
        SoundManager.instance.DeathSound(numPlayer[player]);
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
            Instantiate(Platforms[UnityEngine.Random.Range(0, Platforms.Length)], new Vector3(-8 + 4 * i, -7.5f, -1), Quaternion.identity);
        }
    }

    private IEnumerator AfficherScore()
    {
        SoundManager.instance.ScoreSound();
        int i = 0;
        foreach (var elem in numPlayer)
        {
            playerScoreUI[elem.Value].fillAmount = (float)score[elem.Key]/winScore;
            i++;
        }

        while (i < 4)
        {
            playerScoreUI[i].fillAmount = 0f;
            i++;
        }
        scoreUI.SetActive(true);
        
        yield return new WaitForSeconds(3f);
        
        scoreUI.SetActive(false);
        
        if (winner != null)
        {
            CurrentState = GameState.GameOver;
        }
        else
        {
            CurrentState = GameState.Build;
        }
    }
    public void OnPlayerJoined(PlayerInput input)
    {
        int index = input.playerIndex;

        Color assignedColor = playerColors[index % playerColors.Length];
        
        input.gameObject.GetComponent<PlayerController>().SetColor(assignedColor); //modifie le script du prefab playercontroller comme ca ca change la couleur t'as capté
        numPlayer.Add(input.gameObject, numPlayer.Count);
    }
}
