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
            Debug.Log(gameObject.name);
            Debug.Log(collision.name);
            Debug.Log("GAME OVER: Player killed by Guard!");
            SceneManager.LoadScene("GameOver");
            //Game Over
        }
    }
}
