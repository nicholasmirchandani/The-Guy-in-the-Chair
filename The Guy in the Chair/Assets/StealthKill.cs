using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StealthKill : MonoBehaviour
{
    CircleCollider2D cc;
    int layerMask;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponentInChildren<CircleCollider2D>();
        layerMask = layerMask = LayerMask.GetMask("EnemyHitbox");
    }

    // Update is called once per frame
    void Update()
    {
   
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Charge the enemy
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
            Vector3Int position = GameManager.Instance.grid.WorldToCell(worldPoint);
            Tile tile = (Tile)GameManager.Instance.tilemap.GetTile(position);

            //If you didn't click on the hitbox, move as normal
            if(Physics2D.OverlapCircle(new Vector2(position.x, position.y), 0.1f, layerMask) == null)
            {
                if (tile != null)
                {
                    if (PlayerManager.Instance.isTracking)
                    {
                        PlayerManager.Instance.isTracking = false;
                        GameManager.Instance.tracker.transform.parent = null;
                    }
                    GameManager.Instance.tracker.SetActive(true);
                    GameManager.Instance.tracker.transform.position = position + new Vector3(0.5f, 0.5f, -5);
                    GameManager.Instance.playerAI.SearchPath();
                }
            }
            else
            {
                GameManager.Instance.tracker.SetActive(true);
                GameManager.Instance.tracker.transform.position = position + new Vector3(0.5f, 0.5f, -5);
                GameManager.Instance.tracker.transform.parent = this.gameObject.transform;
                PlayerManager.Instance.isTracking = true;
                Debug.Log("TRACKING IS A GO!");
            }

        }
    }
}
