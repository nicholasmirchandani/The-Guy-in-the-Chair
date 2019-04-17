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
        layerMask = LayerMask.GetMask("Player", "Wall");
    }

    private void Update()
    {
        if (searchForPlayer && ai.reachedEndOfPath)
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
                        GetComponent<AILerp>().speed = 3;
                    }
                    else if (col.GetComponent<PlayerManager>().isHidden && wasTrackingPlayer) //If the player is hidden but they never left the guard's line of sight
                    {
                        Debug.Log("Game Over: Player tried to hide in front of guard"); //TODO: Animations to make this make more sense.  You're not supposed to be able to run into and out of cover quiclky or easily
                        SceneManager.LoadScene("GameOver");
                        //Game Over
                    }
                    else if (wasTrackingPlayer)
                    {
                        if (!losingPlayer)
                        {
                            StartCoroutine("LosePlayer");
                        }
                    }
                }
                else
                {
                    wasTrackingPlayer = false;
                }
            }

            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player") && wasTrackingPlayer)
        {
            searchForPlayer = true;
            Debug.Log("Player Exit!");
            player = collision.gameObject;
                if (!losingPlayer)
                {
                    StartCoroutine("LosePlayer");
                }
            wasTrackingPlayer = false;
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
