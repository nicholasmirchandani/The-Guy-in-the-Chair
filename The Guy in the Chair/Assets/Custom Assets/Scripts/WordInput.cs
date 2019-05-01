using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordInput : MonoBehaviour
{
    [SerializeField] private bool isActive;
    public Camera WordCamera;
    public Text StringMessage;
    // Update is called once per frame
    void Update()
    {
        if(WordCamera.enabled)
        {
            isActive = true;
            foreach (char letter in Input.inputString)
            {
                if(letter == StringMessage.text[0])
                {
                    StringMessage.text = StringMessage.text.Substring(1);
                }
            }
        }
        else
        {
            isActive = false;
        }
    }

}