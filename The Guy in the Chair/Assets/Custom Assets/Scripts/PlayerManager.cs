using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public bool isHidden;
    public bool isTracking; //is tracking enemy for stealth kill
    public bool needsUpdate; //Needs update from destination change in TileClicking/Cover/Winzone/Collectible/Stairs/StealthKill
    public bool isCarryingBody;
    public GameObject bodyBeingCarried;
    public Action queuedAction;

    private GraphNode currentNode;
    private GraphNode previousNode;

    public enum Action {
        NONE,
        PICKUP_BODY,
        HIDE_BODY, //IN COVER
        TAKE_BODY, //FROM COVER
        KILL_GUARD
    }

    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.frameCount % 5 == 0)
        {
            if(isTracking || needsUpdate)
            {
                currentNode = AstarPath.active.GetNearest(GameManager.Instance.player.transform.position).node;
                if (previousNode != currentNode)
                {
                    GetComponent<IAstarAI>().SearchPath();
                    needsUpdate = false;
                }
                previousNode = AstarPath.active.GetNearest(GameManager.Instance.player.transform.position).node;

            }
        }
    }
}
