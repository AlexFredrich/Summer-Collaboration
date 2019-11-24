using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPanel : MonoBehaviour
{
    //Editor Variables
    [SerializeField] private GameObject[] activateObjects;
    [SerializeField] private GameObject[] batteryHolders;

    //Private Variables
    private Animator animator;
    private IActivatable[] activateObjectsI;
    private bool isActive;
    private bool actionDone;

    void Start()
    {
        animator = GetComponent<Animator>();
        isActive = false;
        actionDone = true;

        for (int i = 0; i < activateObjects.Length; i++)
        {
            activateObjectsI[i] = activateObjects[i].GetComponent<IActivatable>();                                         
        }
    }

    void Update()
    {
        if (!actionDone && !isActive)
        {
            foreach (GameObject batteryHolder in batteryHolders)
            {
                if (batteryHolder.transform.childCount > 0)
                {
                    isActive = true;
                }
                else
                {
                    isActive = false;
                    break;
                }
            }
        }

        if (isActive && actionDone)
        {
            isActive = true;
            actionDone = false;

            animator.SetBool("IsEnabled", true);

            ActivateObjects();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SteamBattery")
        {
            //Parent Steam Battery to Battery Holder
            foreach (GameObject batteryHolder in batteryHolders)
            {
                if (batteryHolder.transform.childCount == 0)
                {
                    other.transform.parent = batteryHolder.transform;
                    other.GetComponent<Rigidbody>().isKinematic = true;
                    break;
                }
            }

            //Fix Steam Battery Position
            other.transform.position = new Vector3(0, 0, 0);
            other.transform.rotation = Quaternion.Euler(0, 0, 0);

            actionDone = false;
        }
    }

    private void ActivateObjects()
    {
        foreach (IActivatable activateObjectI in activateObjectsI)
        {
            activateObjectI.Activate();
        }
    }
}


