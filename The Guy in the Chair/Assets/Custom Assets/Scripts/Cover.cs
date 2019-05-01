using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Cover : MonoBehaviour
{

    public bool holdingBody;
    public GameObject bodyBeingCarried;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            collision.GetComponent<PlayerManager>().isHidden = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            collision.GetComponent<PlayerManager>().isHidden = false;
        }
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0) && !GameManager.Instance.gameOver)
        {
            if(!PlayerManager.Instance.isCarryingBody && !holdingBody)
            {
                //Take Cover behind the cover
                Ray ray = GameManager.Instance.levelCamera.ScreenPointToRay(Input.mousePosition);
                Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
                Vector3Int position = GameManager.Instance.grid.WorldToCell(worldPoint);

                //Stop going after an enemy if you click off them
                if (PlayerManager.Instance.isTracking)
                {
                    PlayerManager.Instance.isTracking = false;
                    GameManager.Instance.tracker.transform.parent = null;
                }

                GameManager.Instance.tracker.SetActive(true);
                GameManager.Instance.tracker.transform.position = position + new Vector3(0.5f, 0.5f, -5);
                GameManager.Instance.playerAI.SearchPath();
                PlayerManager.Instance.queuedAction = PlayerManager.Action.NONE;
            }
            else if(!holdingBody)
            {
                //Path to object then hide body when close enough
                Ray ray = GameManager.Instance.levelCamera.ScreenPointToRay(Input.mousePosition);
                Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
                Vector3Int position = GameManager.Instance.grid.WorldToCell(worldPoint);

                //Stop going after an enemy if you click off them
                if (PlayerManager.Instance.isTracking)
                {
                    PlayerManager.Instance.isTracking = false;
                    GameManager.Instance.tracker.transform.parent = null;
                }

                GameManager.Instance.tracker.SetActive(true);
                GameManager.Instance.tracker.transform.position = position + new Vector3(0.5f, 0.5f, -5);
                GameManager.Instance.playerAI.SearchPath();
                PlayerManager.Instance.queuedAction = PlayerManager.Action.HIDE_BODY;
                StartCoroutine("HideBody");

            }
            else if (!PlayerManager.Instance.isCarryingBody && holdingBody)
            {
                //Path to object then take body when close enough
                Ray ray = GameManager.Instance.levelCamera.ScreenPointToRay(Input.mousePosition);
                Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
                Vector3Int position = GameManager.Instance.grid.WorldToCell(worldPoint);

                //Stop going after an enemy if you click off them
                if (PlayerManager.Instance.isTracking)
                {
                    PlayerManager.Instance.isTracking = false;
                    GameManager.Instance.tracker.transform.parent = null;
                }

                GameManager.Instance.tracker.SetActive(true);
                GameManager.Instance.tracker.transform.position = position + new Vector3(0.5f, 0.5f, -5);
                GameManager.Instance.playerAI.SearchPath();

                PlayerManager.Instance.queuedAction = PlayerManager.Action.TAKE_BODY;
                StartCoroutine("TakeBody");

                //Pickup body from cover
            }
        }
    }

    private IEnumerator HideBody() { //Using a coroutine for parallel computing
        while(PlayerManager.Instance.queuedAction == PlayerManager.Action.HIDE_BODY)
        {
            if (Vector3.Distance(GameManager.Instance.player.gameObject.transform.position, gameObject.transform.position) <= 1.5)
            {
                //Throw body into cover
                PlayerManager.Instance.isCarryingBody = false;
                holdingBody = true;
                PlayerManager.Instance.bodyBeingCarried.transform.parent = this.gameObject.transform;
                this.bodyBeingCarried = PlayerManager.Instance.bodyBeingCarried;
                this.bodyBeingCarried.transform.position = this.gameObject.transform.position + new Vector3(0, 0, 0.5f); //TODO: Add throwing animation
                PlayerManager.Instance.bodyBeingCarried = null;
                Debug.Log("Throwing body into cover");
                PlayerManager.Instance.queuedAction = PlayerManager.Action.NONE;
                GameManager.Instance.tracker.SetActive(false);
                GameManager.Instance.tracker.transform.position = GameManager.Instance.player.transform.position;
                GameManager.Instance.playerAI.SearchPath();
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }

    private IEnumerator TakeBody()
    {
        while(PlayerManager.Instance.queuedAction == PlayerManager.Action.TAKE_BODY)
        {
            if(Vector3.Distance(GameManager.Instance.player.gameObject.transform.position, gameObject.transform.position) <= 1.5)
            {
                PlayerManager.Instance.isCarryingBody = true;
                holdingBody = false;
                PlayerManager.Instance.bodyBeingCarried = this.bodyBeingCarried;
                this.bodyBeingCarried = null;
                PlayerManager.Instance.bodyBeingCarried.transform.parent = GameManager.Instance.player.transform;
                PlayerManager.Instance.bodyBeingCarried.transform.position = GameManager.Instance.player.transform.position; //TODO: Add removing animation
                Debug.Log("Picking up body from cover");
                PlayerManager.Instance.queuedAction = PlayerManager.Action.NONE;
                GameManager.Instance.tracker.SetActive(false);
                GameManager.Instance.tracker.transform.position = GameManager.Instance.player.transform.position;
                GameManager.Instance.playerAI.SearchPath();
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }

}
