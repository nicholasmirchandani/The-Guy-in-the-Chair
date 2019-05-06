using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreen : MonoBehaviour
{
    [SerializeField] private bool enableCursor;
    [SerializeField] private bool isEnabled;
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
        if (GameManager.Instance.isPaused || GameManager.Instance.gameOver)
        {
            //Don't do anything
        }
        else
        {
            if (GameManager.Instance.levelCamera.enabled)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    GameManager.Instance.levelCamera.enabled = false;
                    mainCamera.enabled = true;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    GameManager.Instance.controlHint.text = "Left click into monitors";
                }
            }
        }
    }

    private void OnMouseOver()
    {
        if (!GameManager.Instance.isPaused && !GameManager.Instance.gameOver && isEnabled)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameManager.Instance.controlHint.text = "Escape to return";
                mainCamera.enabled = false;
                GameManager.Instance.levelCamera.enabled = true;
                x = 0;
                y = 0;
                mainCamera.transform.eulerAngles = new Vector3(-y, x, 0);
                if (enableCursor)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
            }
        }
    }

    public void EnableScreen()
    {
        isEnabled = true;
        GetComponentInParent<MeshRenderer>().enabled = true;
    }
}
