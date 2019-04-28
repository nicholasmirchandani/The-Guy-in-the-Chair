using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerKill : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            if(GetComponentInParent<EnemyChase>().wasTrackingPlayer)
            {
                Debug.Log("GAME OVER: Player killed by Guard!");
                SceneManager.LoadScene("GameOver");
            }
            else
            {
                Instantiate(GameManager.Instance.bodyPrefab, this.gameObject.transform.parent.transform.position + new Vector3(0,0,0.5f), Quaternion.identity);
                Destroy(this.gameObject.transform.parent.gameObject); //TODO: Change to drop a body behind
                Debug.Log("Guard Killed By Player!");
                
                
                //Stop going after the enemy after you kill them
                if (PlayerManager.Instance.isTracking)
                {
                    PlayerManager.Instance.isTracking = false;
                    GameManager.Instance.tracker.transform.parent = null;
                    GameManager.Instance.tracker.SetActive(false);
                }

                GameManager.Instance.chaosLevel += 20 / GameManager.Instance.chaosDefense;
            }
            //Game Over
        }
    }
}
