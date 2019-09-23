using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField]
    Transform pickupPoint;
    [SerializeField]
    Transform lookPoint;
    [SerializeField]
    float speed = .5f;
    [SerializeField]
    float rotationSlowSpeed = .8f;

    private PickupObj currentPickupObj;
    private Rigidbody pickupRigidBody;
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
            pickupRigidBody.MovePosition(lerpTransform);
        }
    }

    void LiftObj(PickupObj pickupObj)
    {
        pickupRigidBody = pickupObj.gameObject.GetComponent<Rigidbody>();

        if (pickupObj.CurrentState == PickupObj.State.Neutral)
        {
            pickupObj.SetPickedUp();
            currentPickupObj = pickupObj;
            isLiftingObj = true;
            pickupRigidBody.useGravity = false;
            pickupRigidBody.velocity = Vector3.zero;
            pickupRigidBody.angularDrag = rotationSlowSpeed;
            pickupRigidBody.drag = 1f;
        }
        else if (pickupObj.CurrentState == PickupObj.State.PickedUp)
        {
            pickupObj.SetNeutral();
            isLiftingObj = false;
            pickupRigidBody.useGravity = true;
            currentPickupObj = null;
        }
        else if (pickupObj.CurrentState == PickupObj.State.Frozen)
        {
            // do stuff here for frozen object
        }
    }

    private void OnEnable()
    {
        PickupObj.PickUpObjActivated += LiftObj;
    }
}
