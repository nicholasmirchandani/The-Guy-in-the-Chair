using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileClicking : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.playerAI.reachedDestination)
        {
            GameManager.Instance.tracker.SetActive(false);
        }
    }

    //Optimization: Only call onMouseOver if mouse is over tilemap
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
            Vector3Int position = GameManager.Instance.grid.WorldToCell(worldPoint);
            Tile tile = (Tile)GameManager.Instance.tilemap.GetTile(position);

            if (tile != null)
            {
                if (tile.name.Equals("BIGFLOOR_STANDIN"))
                {
                    GameManager.Instance.tracker.SetActive(true);
                    GameManager.Instance.tracker.transform.position = position + new Vector3(0.5f, 0.5f, -5);
                    GameManager.Instance.playerAI.SearchPath();
                }
                else
                {
                    GameManager.Instance.tracker.SetActive(false);
                }
            }
        }
    }
}
