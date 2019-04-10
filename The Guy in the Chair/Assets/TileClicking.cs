using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileClicking : MonoBehaviour
{
    // Start is called before the first frame update

    Grid m_level;
    Tilemap m_TileMap;
    public GameObject tracker;
    public GameObject player;
    IAstarAI ai;

    void Start()
    {
        m_level = GetComponentInParent<Grid>();
        m_TileMap = GetComponent<Tilemap>();
        ai = player.GetComponent<IAstarAI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
            Vector3Int position = m_level.WorldToCell(worldPoint);
            Tile tile = (Tile)m_TileMap.GetTile(position);

            if (tile != null)
            {
                if(tile.name.Equals("BIGFLOOR_STANDIN"))
                {
                    tracker.SetActive(true);
                    tracker.transform.position = position + new Vector3(0.5f,0.5f,-5);
                    ai.SearchPath();
                }
                else
                {
                    tracker.SetActive(false);
                }
            }
        }

        if (ai.reachedDestination)
        {
            tracker.SetActive(false);
        }
    }
}
