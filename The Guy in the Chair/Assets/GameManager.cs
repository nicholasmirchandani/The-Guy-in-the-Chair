using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int chaosLevel = 0;
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
            chaosLevel = 100;
            Debug.Log("GAME OVER: You've been caught :(");
        }
    }
}
