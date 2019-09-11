using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBridgeChecker : MonoBehaviour
{
    [SerializeField]
    private Transform triggerCheck;

    public bool isBlocked = true;

    private int blockerCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Player")
        {
            isBlocked = true;
            blockerCount++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag != "Player")
        {
            blockerCount--;
            if(blockerCount == 0)
            {
                isBlocked = false;
            }
        }
    }
}
