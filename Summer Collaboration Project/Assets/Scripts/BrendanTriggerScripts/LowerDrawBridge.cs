using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerDrawBridge : MonoBehaviour, IActivatable
{
    [SerializeField]
    private Transform BridgeOne;
    [SerializeField]
    private Transform BridgeTwo;
    [SerializeField]
    private DrawBridgeChecker drawBridgeChecker;

    public void Activate()
    {
        if(!drawBridgeChecker.isBlocked)
        {
            //lower drawbridges
        }
        else
        {
            //feedback to indicate blockage
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
