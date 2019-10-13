using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LowerDrawbridge : MonoBehaviour
{
    [SerializeField]
    private Transform BridgeOne;
    [SerializeField]
    private Transform BridgeTwo;
    [SerializeField]
    private DrawbridgeClearenceCheck drawBridgeChecker;

    public void Activate()
    {
        if (!drawBridgeChecker.isBlocked)
        {
            //lower drawbridges
        }
        else
        {
            //feedback to indicate blockage
        }
    }

    public void InterferenceDetected()
    {
        //check if raising is needed
        //change state if needed
        //raise drawbridges at 2x speed
        //lights flash?
        //sound plays?
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        drawBridgeChecker.bridgeInterference.AddListener(InterferenceDetected);
    }
    private void OnDisable()
    {
        drawBridgeChecker.bridgeInterference.RemoveListener(InterferenceDetected);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void RaiseBridge()
    {
        drawBridgeChecker.currentState = DrawbridgeClearenceCheck.bridgeState.raising;
    }

    void LowerBridge()
    {
        drawBridgeChecker.currentState = DrawbridgeClearenceCheck.bridgeState.lowering;
    }


}
