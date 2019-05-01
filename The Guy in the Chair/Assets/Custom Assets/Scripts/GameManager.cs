using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isPaused;

    [Header("Game Resources")]
    public double chaosLevel = 0;
    public double chaosDefense = 1;
    public int electricityLevel = 10;
    public int wadesCount = 0;

    [Header("Game Assets")]
    public GameObject player;
    public IAstarAI playerAI;
    public GameObject tracker;
    public Camera mainCamera;
    public Camera levelCamera;
    public GameObject bodyPrefab;
    public GameObject PauseMenu;

    [SerializeField] public Grid grid;
    [SerializeField] public Tilemap tilemap;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        playerAI = player.GetComponent<IAstarAI>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(chaosLevel >= 100)
        {
            chaosLevel = 0;
            Debug.Log("GAME OVER: Chaos Level too High :(");
            SceneManager.LoadScene("GameOver");
            //Game Over
        }

        if (Input.GetKeyDown(KeyCode.Escape) && mainCamera.enabled)
        {
            isPaused = true;
            PauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PauseMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
