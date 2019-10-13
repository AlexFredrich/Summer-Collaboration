using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DrawbridgeClearenceCheck : MonoBehaviour
{
    public UnityEvent bridgeInterference;

    [SerializeField]
    private Transform triggerCheck;

    public bool isBlocked = true;
    public enum bridgeState { lowered, lowering, raising, raised}
    public bridgeState currentState;

    private int blockerCount = 0;

    private void Start()
    {
        if(bridgeInterference == null)
        {
            bridgeInterference = new UnityEvent();
        }
        currentState = bridgeState.raised;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            if(currentState != bridgeState.raised || currentState != bridgeState.raising)
            {
                bridgeInterference.Invoke();
                isBlocked = true;
            }        
            blockerCount++;
            //change state if needed
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player")
        {
            blockerCount--;
            if (blockerCount == 0)
            {
                isBlocked = false;
            }
            //change state
        }
    }
}
