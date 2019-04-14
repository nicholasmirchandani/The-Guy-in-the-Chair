using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.SceneManagement;

public class EnemyChase : MonoBehaviour
{
    public GameObject tracker;
    private GameObject player;
    private bool searchForPlayer;
    private bool losingPlayer = false;
    public IAstarAI ai;

    public Transform[] patrolPoints;
    private int currentPatrolIndex;
    private Transform currentPatrolPoint;

    private void Start()
    {
        ai = GetComponent<IAstarAI>();
        currentPatrolIndex = 0;
        searchForPlayer = false;
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
                GameManager.Instance.chaosLevel += 1/GameManager.Instance.chaosDefense;
                tracker.transform.position = col.transform.position;
                ai.SearchPath();
            }
            else if (col.tag.Equals("Player") && !searchForPlayer) //If the player is hidden but they never left the guard's line of sight
            {
                Debug.Log("Game Over: Player tried to hide in front of guard"); //TODO: Animations to make this make more sense.  You're not supposed to be able to run into and out of cover quiclky or easily
                SceneManager.LoadScene("GameOver");
                //Game Over
            }
            else if(col.tag.Equals("Player"))
            { 
                Debug.Log("Tracker: " + tracker.transform.position);
                Debug.Log("Player: " + col.transform.position);
                if(!losingPlayer)
                {
                    StartCoroutine("LosePlayer");
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            searchForPlayer = true;
            player = collision.gameObject;
                if (!losingPlayer)
                {
                    StartCoroutine("LosePlayer");
                }
            }
        }

    IEnumerator LosePlayer()
    {
        losingPlayer = true;
        Debug.Log("Losing Player");
        yield return new WaitForSeconds(3f);
        searchForPlayer = false;
        NextPatrolPoint();
        Debug.Log("Player Lost");
        losingPlayer = false;
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
