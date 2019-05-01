using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScreen : MonoBehaviour
{
    [SerializeField] private bool enableCursor;
    [SerializeField] private Camera targetCamera;
    public Camera mainCamera;
    float x = 0;
    float y = 0;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.isPaused)
        {
            //Don't do anything
        }
        else
        {
            if (targetCamera.enabled)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    targetCamera.enabled = false;
                    mainCamera.enabled = true;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }
        }
    }

    private void OnMouseOver()
    {
        if(!GameManager.Instance.isPaused)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("SWITCH!");
                mainCamera.enabled = false;
                targetCamera.enabled = true;
                x = 0;
                y = 0;
                mainCamera.transform.eulerAngles = new Vector3(-y, x, 0);
                if(enableCursor)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
            }
        }
    }
}
