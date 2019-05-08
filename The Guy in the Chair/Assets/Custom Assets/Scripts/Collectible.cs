using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && !GameManager.Instance.gameOver)
        {
            //Move to the collectible
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
            PlayerManager.Instance.needsUpdate = true;
            PlayerManager.Instance.queuedAction = PlayerManager.Action.NONE;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            GameManager.Instance.collectibles += 1;
            GameManager.Instance.OnCollect();
            gameObject.SetActive(false);
        }
    }
}
