using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordInput : MonoBehaviour
{
    [SerializeField] private bool isActive;
    public Camera WordCamera;
    // Update is called once per frame
    void Update()
    {
        if(WordCamera.enabled)
        {
            isActive = true;
            foreach (char letter in Input.inputString)
            {
                Debug.Log(letter);//This is the letter we want to deal with
            }
        }
        else
        {
            isActive = false;
        }
    }

}