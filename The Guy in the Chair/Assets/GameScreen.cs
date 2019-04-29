using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScreen : MonoBehaviour
{
    public Camera mainCamera;
    float x = 0;
    float y = 0;

    public GameObject PauseMenu;
    private bool isPaused = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isPaused)
        {
            //Don't do anything
        }
        else
        {
            if (mainCamera.enabled)
            {
                x += 5 * Input.GetAxis("Mouse X");
                y += 5 * Input.GetAxis("Mouse Y");
                mainCamera.transform.eulerAngles = new Vector3(-y, x, 0);
            }
            if (GameManager.Instance.levelCamera.enabled)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    GameManager.Instance.levelCamera.enabled = false;
                    mainCamera.enabled = true;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    isPaused = true;
                    PauseMenu.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
            }
        }
    }

    private void OnMouseOver()
    {
        if(!isPaused)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("SWITCH!");
                mainCamera.enabled = false;
                GameManager.Instance.levelCamera.enabled = true;
                x = 0;
                y = 0;
                mainCamera.transform.eulerAngles = new Vector3(-y, x, 0);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
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
