using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{

    private Camera mainCamera;
    float x;
    float y;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GetComponent<Camera>();
        x = 0;
        y = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCamera.enabled && !GameManager.Instance.isPaused && !GameManager.Instance.gameOver)
        {
            x += 5 * Input.GetAxis("Mouse X");
            y += 5 * Input.GetAxis("Mouse Y");
            if(y >= 90)
            {
                y = 90;
            }
            else if (y <= -90)
            {
                y = -90;
            }
            mainCamera.transform.eulerAngles = new Vector3(-y, x, 0);
        }
    }
}
