using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hackable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Hack();
        }
    }

    protected void Hack()
    {
        if(GameManager.Instance.electricityLevel > 0)
        {
            Debug.Log(name + " Hacked");
            this.gameObject.SetActive(false);
            --GameManager.Instance.electricityLevel;
        }
        else
        {
            Debug.Log("You don't have enough electricity :(");
        }
    }
}
