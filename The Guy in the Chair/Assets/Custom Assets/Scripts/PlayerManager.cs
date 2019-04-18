using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public bool isHidden;
    public bool isTracking; //is tracking enemy for stealth kill
    public bool isCarryingBody;
    public GameObject bodyBeingCarried;

    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.frameCount % 5 == 0)
        {
            if(isTracking)
            {
                GetComponent<IAstarAI>().SearchPath();
            }
        }
    }
}
