using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordGame : MonoBehaviour
{
    public static WordGame Instance;
    private Text stringToType;
    public Text scoreText;
    public int score;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        stringToType = GetComponent<Text>();
        stringToType.text = "Hack the mainframe to disarm the body fuse";
        score = 0;
        scoreText.text = "Score: " + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (stringToType.text == "")
        {
            stringToType.text = "Hack the mainframe to disarm the body fuse";
            ++score;
            scoreText.text = "Score: " + score.ToString();
        }
    }
}
