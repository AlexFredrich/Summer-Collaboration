using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObj : MonoBehaviour, IActivatable
{
    public static event Action<PickupObj> PickUpObjActivated;

    public enum State { Neutral, PickedUp, Frozen};

    State currentState;

    public State CurrentState
    {
        get
        {
            return currentState;
        }
    }

    public void Activate()
    {
        OnPickUpObjActivated(this);
    }

    public void SetNeutral()
    {
        currentState = State.Neutral;
    }

    public void SetPickedUp()
    {
        currentState = State.PickedUp;
    }

    public void SetFrozen()
    {
        currentState = State.Frozen;
    }

    private void OnPickUpObjActivated(PickupObj pickUpObj)
    {
        if (PickUpObjActivated != null)
        {
            PickUpObjActivated.Invoke(pickUpObj);
        }
        
    }
}
