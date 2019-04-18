using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBody : MonoBehaviour
{
    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(!PlayerManager.Instance.isCarryingBody)
            {
                Debug.Log("Move the body");
                PlayerManager.Instance.isCarryingBody = true;
                PlayerManager.Instance.bodyBeingCarried = this.gameObject;
                PlayerManager.Instance.bodyBeingCarried.transform.parent = GameManager.Instance.player.transform;

            }
        }
    }
}
