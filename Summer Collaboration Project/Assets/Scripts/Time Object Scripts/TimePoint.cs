using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class TimePoint
{

    // Recorded state at this time
    [Tooltip("The time state this time point recorded")]
    public TimeState thisTimeState;

    // Position at this time
    [Tooltip("The position this time point recroded")]
    public Vector3 thisPosition;

    // Rotation at this time
    [Tooltip("The rotation this time point recroded")]
    public Quaternion thisRotation;

    // Scale at this time
    [Tooltip("The scale this time point recroded")]
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
