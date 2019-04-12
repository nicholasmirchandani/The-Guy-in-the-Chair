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

    public Transform[] patrolPoints;
    private int currentPatrolIndex;
    private Transform currentPatrolPoint;

    private void Start()
    {
        ai = GetComponent<IAstarAI>();
        currentPatrolIndex = 0;
    }

    private void Update()
    {
        if(searchForPlayer && !player.GetComponent<PlayerManager>().isHidden && ai.reachedEndOfPath)
        {
            Debug.Log("OOF");
            //Some sort of searching script
        }
        else if (transform.position == tracker.transform.position)
        {
            NextPatrolPoint();
            Debug.Log("NEXT!");
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if(Time.frameCount % 5 == 0)
        {
            if (col.tag.Equals("Player") && !col.GetComponent<PlayerManager>().isHidden)
            {
                ++GameManager.Instance.chaosLevel;
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
        NextPatrolPoint();
    }

    public void NextPatrolPoint()
    {
        currentPatrolPoint = patrolPoints[currentPatrolIndex];
        ++currentPatrolIndex;
        if (currentPatrolIndex > patrolPoints.Length - 1)
        {
            currentPatrolIndex = 0;
        }
        tracker.transform.position = currentPatrolPoint.position;
        ai.SearchPath();
    }

}
