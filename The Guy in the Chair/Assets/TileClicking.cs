using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileClicking : MonoBehaviour
{
    // Start is called before the first frame update

    Grid m_level;
    Tilemap m_TileMap;

    void Start()
    {
        m_level = GetComponentInParent<Grid>();
        m_TileMap = GetComponent<Tilemap>();
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
                Debug.Log("Tile Name: " + tile.name);
                m_TileMap.SetTile(position, null);
            }
        }
    }
}
