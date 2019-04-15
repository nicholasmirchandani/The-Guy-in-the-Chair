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
                Destroy(this.gameObject.transform.parent.gameObject);
            }
            //Game Over
        }
    }
}
