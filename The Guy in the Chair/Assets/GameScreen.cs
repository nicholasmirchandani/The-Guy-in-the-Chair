using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScreen : MonoBehaviour
{
    public Camera mainCamera;
    public Camera levelCamera;
    float x = 0;
    float y = 0;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCamera.enabled)
        {
            x += 5 * Input.GetAxis("Mouse X");
            y += 5 * Input.GetAxis("Mouse Y");
            mainCamera.transform.eulerAngles = new Vector3(-y, x, 0);
        }
        if (levelCamera.enabled)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                levelCamera.enabled = false;
                mainCamera.enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("SWITCH!");
            mainCamera.enabled = false;
            levelCamera.enabled = true;
            x = 0;
            y = 0;
            mainCamera.transform.eulerAngles = new Vector3(-y, x, 0);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
