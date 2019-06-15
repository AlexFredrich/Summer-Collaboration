using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField]
    Transform pickupPoint;
    [SerializeField]
    float speed = .5f;

    private PickupObj currentPickupObj;
    private Rigidbody PickupRigidBody;
    private bool isLiftingObj = false;

    public bool IsLiftingObj
    {
        get
        {
            return isLiftingObj;
        }
    }

    public PickupObj CurrentPickupObj
    {
        get
        {
            return currentPickupObj;
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (isLiftingObj == true && currentPickupObj != null)
        {
            Vector3 lerpTransform = Vector3.Lerp(currentPickupObj.transform.position, pickupPoint.position, speed);
            PickupRigidBody.MovePosition(lerpTransform);
        }
    }

    void LiftObj(PickupObj pickupObj)
    {
        PickupRigidBody = pickupObj.gameObject.GetComponent<Rigidbody>();

        if (pickupObj.CurrentState == PickupObj.State.Neutral)
        {
            pickupObj.SetPickedUp();
            currentPickupObj = pickupObj;
            isLiftingObj = true;
            PickupRigidBody.useGravity = false;
        }
        else if (pickupObj.CurrentState == PickupObj.State.PickedUp)
        {
            pickupObj.SetNeutral();
            isLiftingObj = false;
            PickupRigidBody.useGravity = true;
            currentPickupObj = null;
        }
    }

    private void OnEnable()
    {
        PickupObj.PickUpObjActivated += LiftObj;
    }
}
