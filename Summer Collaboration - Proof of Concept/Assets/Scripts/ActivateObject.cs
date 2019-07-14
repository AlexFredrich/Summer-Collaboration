﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObject : MonoBehaviour
{
    [SerializeField]
    float maxActivateDistance = 5f;

    [SerializeField]
    PickUp pickUp;

    [SerializeField]
    TimeObjectManager TimeObjectManager;

    IActivatable objectToActivate;
    TimeObject objectToFreeze;

    private void Update()
    {
        GetObjectToActivate();
        ActivateItem();
    }

    private void GetObjectToActivate()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, maxActivateDistance))
        {
            Debug.Log("Hit: " + hit.transform.name);

            objectToActivate = hit.transform.GetComponent<IActivatable>();
            objectToFreeze = hit.transform.GetComponent<TimeObject>();
        }
        else
        {
            objectToActivate = null;
            objectToFreeze = null;
        }
    }

    private void ActivateItem()
    {
        if (objectToActivate != null)
        {
            if (Input.GetButtonDown("Activate") && pickUp.IsLiftingObj == true)
            {
                pickUp.CurrentPickupObj.Activate();
            }
            else if (Input.GetButtonDown("Activate"))
            {
                objectToActivate.Activate();

            }
        }
    }
    private void FreezeObject()
    {
        if (objectToFreeze != null)
        {
            if (Input.GetButtonDown("Freeze"))
            {
                if (TimeObjectManager.FrozenObjects.Contains(objectToFreeze))
                {
                    objectToFreeze.UnfreezeTime();
                }
                else
                {
                    objectToFreeze.FreezeTime();
                }
                
            }
        }
       
    }
}
