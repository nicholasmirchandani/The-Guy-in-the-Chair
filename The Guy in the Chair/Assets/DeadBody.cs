using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBody : MonoBehaviour
{
    public bool found = false;
    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(!PlayerManager.Instance.isCarryingBody)
            {


                //Get to the body
                Ray ray = GameManager.Instance.levelCamera.ScreenPointToRay(Input.mousePosition);
                Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
                Vector3Int position = GameManager.Instance.grid.WorldToCell(worldPoint);
                GameManager.Instance.tracker.SetActive(true);
                GameManager.Instance.tracker.transform.position = position + new Vector3(0.5f, 0.5f, -5);
                GameManager.Instance.playerAI.SearchPath();

                Debug.Log(GameManager.Instance.playerAI.reachedEndOfPath); //When this is true && the player is within range to pick up enemy, unless interrputed, execute the following code.  Use events?
                
                    Debug.Log("Move the body");
                    PlayerManager.Instance.isCarryingBody = true;
                    PlayerManager.Instance.bodyBeingCarried = this.gameObject;
                    PlayerManager.Instance.bodyBeingCarried.transform.parent = GameManager.Instance.player.transform;
                    this.transform.position = PlayerManager.Instance.transform.position;
                

            }
        }
    }
}
