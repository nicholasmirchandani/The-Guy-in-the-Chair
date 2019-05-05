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

    [SerializeField] public Grid grid;
    [SerializeField] public Tilemap tilemap;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        playerAI = player.GetComponent<IAstarAI>();
        StartCoroutine("CountdownTimer");

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
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
        WinGameMessage.text = "You win!\nScore: " + ((100-chaosLevel) + timeRemaining);
        GameManager.Instance.gameOver = true;
    }

    public void PauseGame()
    {
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
        player.GetComponent<AILerp>().enabled = true;
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<AILerp>().enabled = true;
        }
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PauseMenu.SetActive(false);
        StartCoroutine("CountdownTimer");
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
