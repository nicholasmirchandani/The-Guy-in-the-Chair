using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyChase : MonoBehaviour
{
    public GameObject tracker;
    private GameObject player;
    private bool searchForPlayer;
    public IAstarAI ai;

    private void Start()
    {
        ai = GetComponent<IAstarAI>();
    }

    private void Update()
    {
        if(searchForPlayer)
        {
            tracker.transform.position = player.transform.position;
            ai.SearchPath();
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if(Time.frameCount % 5 == 0)
        {
            if (col.tag.Equals("Player"))
            {
                tracker.transform.position = col.transform.position;
                ai.SearchPath();

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            searchForPlayer = true;
            player = collision.gameObject;
            StartCoroutine("LosePlayer");
        }
    }

    IEnumerator LosePlayer()
    {
        yield return new WaitForSeconds(0.5f);
        searchForPlayer = false;
    }
}
