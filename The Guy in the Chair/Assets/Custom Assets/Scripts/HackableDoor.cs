using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackableDoor : MonoBehaviour
{

    private Animator animator;
    private bool isClosed;
    public GameObject AStar;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isClosed = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isClosed && GameManager.Instance.electricityLevel > 0)
            {
                Hack();
            }
            else if (GameManager.Instance.electricityLevel <= 0)
            {
                Debug.Log("Not enough Electricity :(");
            }

        }
    }

    void Hack()
    {
        animator.SetTrigger("OpenDoor");
        isClosed = false;
        GetComponent<BoxCollider2D>().enabled = false;
        AStar.GetComponent<AstarPath>().UpdateGraphs(GetComponent<BoxCollider2D>().bounds);
        --GameManager.Instance.electricityLevel;
        PlayerManager.Instance.needsUpdate = true;
    }

}
