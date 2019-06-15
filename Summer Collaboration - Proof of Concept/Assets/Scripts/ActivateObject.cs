using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObject : MonoBehaviour
{
    [SerializeField]
    float maxActivateDistance = 5f;


    IActivatable objectToActivate;

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
        }
        else
        {
            objectToActivate = null;
        }
    }

    private void ActivateItem()
    {
        if (objectToActivate != null)
        {
            if (Input.GetButtonDown("Activate"))
            {
                objectToActivate.Activate();

            }
        }
    }


}
