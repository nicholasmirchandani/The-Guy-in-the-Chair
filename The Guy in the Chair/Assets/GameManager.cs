using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public double chaosLevel = 0;
    public double chaosDefense = 1;
    public int electricityLevel = 10;
    public int wadesCount = 0;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(chaosLevel >= 100)
        {
            chaosLevel = 0;
            Debug.Log("GAME OVER: Chaos Level too High :(");
            SceneManager.LoadScene("GameOver");
            //Game Over
        }
    }
}
