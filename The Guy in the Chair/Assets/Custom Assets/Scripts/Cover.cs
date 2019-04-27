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
        if(Input.GetMouseButtonDown(0))
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
            }
            else if(!holdingBody)
            {
                //TODO: Path to object then trigger when close enough via event
                PlayerManager.Instance.isCarryingBody = false;
                holdingBody = true;
                PlayerManager.Instance.bodyBeingCarried.transform.parent = this.gameObject.transform;
                this.bodyBeingCarried = PlayerManager.Instance.bodyBeingCarried;
                this.bodyBeingCarried.transform.position = this.gameObject.transform.position + new Vector3(0, 0, 0.5f); //TODO: Add throwing animation
                PlayerManager.Instance.bodyBeingCarried = null;
                Debug.Log("Throwing body into cover");
                //Throw body into cover
            }
            else if (!PlayerManager.Instance.isCarryingBody && holdingBody)
            {
                //TODO: Path to object then trigger when close enough via event
                PlayerManager.Instance.isCarryingBody = true;
                holdingBody = false;
                PlayerManager.Instance.bodyBeingCarried = this.bodyBeingCarried;
                this.bodyBeingCarried = null;
                PlayerManager.Instance.bodyBeingCarried.transform.parent = GameManager.Instance.player.transform;
                PlayerManager.Instance.bodyBeingCarried.transform.position = GameManager.Instance.player.transform.position; //TODO: Add removing animation
                Debug.Log("Picking up body from cover");
                //Pickup body from cover
            }
        }
    }
}
