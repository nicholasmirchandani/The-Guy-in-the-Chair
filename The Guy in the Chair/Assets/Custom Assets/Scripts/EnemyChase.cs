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
    public bool wasTrackingPlayer = false;
    private bool isTrackingPlayer = false;
    public IAstarAI ai;

    public Transform[] patrolPoints;
    private int currentPatrolIndex;
    private Transform currentPatrolPoint;

    int layerMask;


    private void Start()
    {
        ai = GetComponent<IAstarAI>();
        currentPatrolIndex = 0;
        searchForPlayer = false;
        layerMask = LayerMask.GetMask("Player", "Wall", "Body");
    }

    private void Update()
    {
        if (searchForPlayer && ai.reachedEndOfPath)
        {
            Debug.Log("Searching Script here");
            if(!losingPlayer)
            {
                StartCoroutine("LosePlayer");
            }
            //Some sort of searching script
        }
        else if (transform.position == tracker.transform.position)
        {
            NextPatrolPoint();
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if(Time.frameCount % 5 == 0)
        {
            if(GameManager.Instance.isPaused)
            {
                return;
            }
            //If the collider is a Player
            if(col.tag.Equals("Player"))
            {
                Debug.DrawRay(transform.position, col.transform.position - transform.position);
                RaycastHit2D hit = Physics2D.Raycast(transform.position, col.transform.position - transform.position, Vector2.Distance(transform.position, col.transform.position - transform.position), layerMask);

                if(hit)
                {
                    if (!col.GetComponent<PlayerManager>().isHidden && hit.collider.tag.Equals("Player"))
                    {
                        GameManager.Instance.chaosLevel += 1 / GameManager.Instance.chaosDefense;
                        tracker.transform.position = col.transform.position;
                        ai.SearchPath();
                        wasTrackingPlayer = true;
                        isTrackingPlayer = true;
                        GetComponent<AILerp>().speed = 3;
                        StopCoroutine("LosePlayer");
                        losingPlayer = false;
                    }
                    else if (col.GetComponent<PlayerManager>().isHidden && isTrackingPlayer) //If the player is hidden but they never left the guard's line of sight
                    {
                        GameManager.Instance.GameOver("Game Over: Player tried to hide in front of guard"); //TODO: Animations to make this make more sense.  You're not supposed to be able to run into and out of cover quiclky or easily
                        //Game Over
                    }
                    else if (isTrackingPlayer)
                    {
                        if (!losingPlayer)
                        {
                            StartCoroutine("LosePlayer");
                        }
                    }
                }
                else
                {
                    isTrackingPlayer = false;
                }
            }

            if(col.tag.Equals("Body") && !col.GetComponent<DeadBody>().found)
            {
                Debug.DrawRay(transform.position, col.transform.position - transform.position);
                RaycastHit2D hit = Physics2D.Raycast(transform.position, col.transform.position - transform.position, Vector2.Distance(transform.position, col.transform.position - transform.position), layerMask);
                if(hit)
                {
                    if(hit.collider.tag.Equals("Body"))
                    {
                        GameManager.Instance.chaosLevel += 10 / GameManager.Instance.chaosDefense;
                        Debug.Log("BODY FOUND");
                        col.GetComponent<DeadBody>().found = true;
                        col.gameObject.SetActive(false);
                        //TODO: Fix so that enemy AI will chase players over enemy bodies
                    }
                }

            }

            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player") && isTrackingPlayer)
        {
            searchForPlayer = true;
            Debug.Log("Player Exit!");
            player = collision.gameObject;
                if (!losingPlayer)
                {
                    StartCoroutine("LosePlayer");
                }
            isTrackingPlayer = false;
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
        wasTrackingPlayer = false;
        isTrackingPlayer = false;
    }

    public void NextPatrolPoint()
    {
        GetComponent<AILerp>().speed = 2;
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
