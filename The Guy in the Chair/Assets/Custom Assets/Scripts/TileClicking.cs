﻿using Pathfinding;
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
        if(GameManager.Instance.isPaused || GameManager.Instance.gameOver)
        {
            return;
        }
        if (GameManager.Instance.playerAI.reachedDestination)
        {
            GameManager.Instance.tracker.SetActive(false);
        }
    }

    //Optimization: Only call onMouseOver if mouse is over tilemap
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && !GameManager.Instance.gameOver)
        {
            Ray ray = GameManager.Instance.levelCamera.ScreenPointToRay(Input.mousePosition);
            Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
            Vector3Int position = GameManager.Instance.grid.WorldToCell(worldPoint);
            Tile tile = (Tile)GameManager.Instance.tilemap.GetTile(position);

            if (tile != null)
            {
                //Stop going after an enemy if you click off them
                if (PlayerManager.Instance.isTracking)
                {
                    PlayerManager.Instance.isTracking = false;
                    GameManager.Instance.tracker.transform.parent = null;
                }

                GameManager.Instance.tracker.SetActive(true);
                GameManager.Instance.tracker.transform.position = position + new Vector3(0.5f, 0.5f, -5);
                if (GameManager.Instance.playerAI.reachedDestination)
                {
                    PlayerManager.Instance.GetComponent<IAstarAI>().SearchPath();
                }
                else
                {
                    PlayerManager.Instance.needsUpdate = true;
                }
            }
            else
            {
                GameManager.Instance.tracker.SetActive(false);
            }

            PlayerManager.Instance.queuedAction = PlayerManager.Action.NONE;
        }
    }
}
