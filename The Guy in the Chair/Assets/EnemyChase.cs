using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyChase : MonoBehaviour
{
    public GameObject tracker;
    private GameObject player;
    public IAstarAI ai;
    private int lookTimer = 0;

    private void Start()
    {
        ai = GetComponent<IAstarAI>();
    }

    private void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.tag.Equals("Player"))
        {
            tracker.transform.position = col.transform.position;
            ai.SearchPath();
        }
    }
}
