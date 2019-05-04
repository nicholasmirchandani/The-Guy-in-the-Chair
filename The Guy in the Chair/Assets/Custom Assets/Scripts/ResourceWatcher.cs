using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceWatcher : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Text>().text = "Time Remaining: " + GameManager.Instance.timeRemaining + "\nChaos level: " + GameManager.Instance.chaosLevel + "\nElectricity: " + GameManager.Instance.electricityLevel + "\nW.A.D.E.S.: " + GameManager.Instance.wadesCount + "\nFiles Acquired: " + GameManager.Instance.collectibles + " / " + GameManager.Instance.collectiblesRequired;
    }
}
