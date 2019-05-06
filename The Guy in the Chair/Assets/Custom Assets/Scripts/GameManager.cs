using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isPaused;
    public bool gameOver;
    public float unpausedTimePassed;
    private bool hasStarted;
    private bool canPause;

    [Header("Game Resources")]
    public double chaosLevel = 0;
    public double chaosDefense = 1;
    public int electricityLevel = 10;
    public int wadesCount = 0;
    public int collectibles = 0;
    public int collectiblesRequired = 3;
    public int timeRemaining = 120;
    public int totalScore = 0;

    [Header("Game Assets")]
    public GameObject player;
    public IAstarAI playerAI;
    public GameObject tracker;
    public Camera mainCamera;
    public Camera levelCamera;
    public GameObject bodyPrefab;
    public GameObject PauseMenu;
    public GameObject GameOverMenu;
    public Text GameOverMessage;
    public GameObject WinGameMenu;
    public Text WinGameMessage;
    public GameObject exitArrows;
    public GameObject[] enemies;
    public MainScreen mainScreen;
    public GameScreen wordScreen;
    public GameScreen resourceScreen;
    public Image logo;
    public AudioClip introMonologue;
    public AudioSource phoneMonologueSource;
    public AudioSource musicSource;
    public Text controlHint;

    [SerializeField] public Grid grid;
    [SerializeField] public Tilemap tilemap;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        playerAI = player.GetComponent<IAstarAI>();
        StartCoroutine("StartGame");

        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        logo.CrossFadeAlpha(0, 0.01f, false);
        logo.enabled = true;
        canPause = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(chaosLevel >= 100)
        {
            chaosLevel = 100;
            GameOver("GAME OVER: Chaos Level too High :(");
            //Game Over
        }

        if (Input.GetKeyDown(KeyCode.Escape) && mainCamera.enabled && !gameOver)
        {
            PauseGame();
        }
    }

    public IEnumerator CountdownTimer()
    {
        while(timeRemaining > 0)
        {
            yield return new WaitForSeconds(1f);
            --timeRemaining;
        }
        GameOver("Ran out of time");
    }

    public void OnCollect()
    {
        if(collectibles >= collectiblesRequired)
        {
            exitArrows.SetActive(true);
        }
    }

    public IEnumerator StartGame()
    {
        //TODO: Play audio clip intro
        phoneMonologueSource.Play();
        //TODO: Add subtitles
        StartCoroutine("TrackUnpausedTimePassed");
        yield return new WaitUntil(() => unpausedTimePassed >= introMonologue.length || Input.GetKeyDown(KeyCode.Space)); //TODO: Replace 1f with the length of the audio clip
        StopCoroutine("TrackUnpausedTimePassed");
        controlHint.text = "Left click into monitors";
	    canPause = false;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            phoneMonologueSource.Pause();
        }
        musicSource.Play();
	    logo.CrossFadeAlpha(1, 2.0f, false);
	    yield return new WaitForSeconds(4.0f);
	    logo.CrossFadeAlpha(0, 2.0f, false);
	    yield return new WaitForSeconds(4.0f);
	    canPause = true;
        mainScreen.EnableScreen();
        resourceScreen.EnableScreen();
        wordScreen.EnableScreen();
        StartCoroutine("CountdownTimer");
        hasStarted = true;
    }

    public IEnumerator TrackUnpausedTimePassed()
    {
        while(true)
        {
            if(!isPaused)
            {
                unpausedTimePassed += Time.deltaTime;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    public void GameOver(string message)
    {
        if(gameOver)
        {
            return;
        }
        GameOverMessage.text = message;
        GameOverMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameOver = true;
        player.GetComponent<AILerp>().enabled = false;
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<AILerp>().enabled = false;
        }
        StopCoroutine("CountdownTimer");
    }

    public void WinGame()
    {
        WinGameMenu.SetActive(true);
        WinGameMessage.text = "You win!\nChaos Level: " + chaosLevel + "\nTime Remaining: " + timeRemaining + "\nScore: " + ((100-chaosLevel) + timeRemaining);
        GameManager.Instance.gameOver = true;
    }

    public void PauseGame()
    {
	    if(!canPause) 
	    {
                return;
	    }
        if(!hasStarted)
        {
            phoneMonologueSource.Pause();
        }
        isPaused = true;
        PauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        player.GetComponent<AILerp>().enabled = false;
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<AILerp>().enabled = false;
        }
        StopCoroutine("CountdownTimer");
    }

    public void ResumeGame()
    {
        if (!hasStarted)
        {
            phoneMonologueSource.UnPause();
        }
        player.GetComponent<AILerp>().enabled = true;
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<AILerp>().enabled = true;
        }
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PauseMenu.SetActive(false);
        if(hasStarted)
        {
            StartCoroutine("CountdownTimer");
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
