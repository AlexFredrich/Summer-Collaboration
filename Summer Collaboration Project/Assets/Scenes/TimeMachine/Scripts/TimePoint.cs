using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TimePoint
{

    // Recorded state at this time
    public TimeState thisTimeState;

    // Position at this time
    public Vector3 thisPosition;

    // Rotation at this time
    public Quaternion thisRotation;

    // Scale at this time
    public Vector3 thisScale;

    public TimePoint(Transform recordedTransform)
    {

        UpdateTransform(recordedTransform);

    }

    public void UpdateTransform(Transform newTransform)
    {

        thisPosition = newTransform.localPosition;
        thisRotation = newTransform.localRotation;
        thisScale = newTransform.localScale;

    }

}
