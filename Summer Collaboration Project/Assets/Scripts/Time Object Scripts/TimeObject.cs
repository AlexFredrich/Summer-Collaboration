﻿using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

[ExecuteInEditMode]
public class TimeObject : MonoBehaviour, ITimeObject
{

#if UNITY_EDITOR

    [SerializeField]
    private bool _customEditor = true;
    public bool CustomEditor { get => _customEditor; }

#endif

    // How much time points should be recorded before culling starts
    [SerializeField]
    [Tooltip("Time points allowed before culling")]
    private int deltaPointLimit = 200;

    [SerializeField]
    [Tooltip("Record even frames where object is not moving")]
    private bool _recordAllTransform;
    public bool RecordAllTransform { get => _recordAllTransform; private set => _recordAllTransform = value; }

    [SerializeField]
    [Tooltip("What was the previous time state")]
    private TimeState _previousTimeState;
    public TimeState PreviousTimeState { get => _previousTimeState; private set => _previousTimeState = value; }

    // What is our current time state
    [SerializeField]
    [Tooltip("What is the current time state")]
    private TimeState _currentTimeState;
    public TimeState CurrentTimeState { get => _currentTimeState; private set => _currentTimeState = value; }

    // What is our current time point
    [SerializeField]
    [Tooltip("What is the current time point")]
    private TimePoint _currentTimePoint;
    public TimePoint CurrentTimePoint { get => _currentTimePoint; private set => _currentTimePoint = value; }

    // Records Initial Timepoint since game started
    [SerializeField]
    [Tooltip("Initial time point when the game started")]
    private TimePoint _originalTimePoint;
    public TimePoint OriginalTimePoint { get => _originalTimePoint; private set => _originalTimePoint = value; }

    // List of time point delta changes
    [SerializeField]
    [Tooltip("Collection of all Time points recorded")]
    private List<TimePoint> _timePointDelta;
    public List<TimePoint> TimePointDelta { get => _timePointDelta; private set => _timePointDelta = value; }

    // Rigidbody reference to parent GameObject
    private Rigidbody thisRigidbody;

    // Interpolation Time
    private float interpolateTime = 0;

    // Track the current index of the TimePointDelta;
    private int currentTimeIndex;

    // Clear history after time reverse is complete?
    private bool clearAfterReverseComplete;

    // Allow other gameobjects to listen into time events
    [Tooltip("When Time object reverse sequence completes")]
    public UnityEvent OnReverseComplete;
    [Tooltip("When Time object forward sequence completes")]
    public UnityEvent OnForwardComplete;

    private void Awake()
    {

        // save initial time point from the moment the game starts
        OriginalTimePoint = new TimePoint(this.gameObject.transform);
        CurrentTimePoint = OriginalTimePoint;

    }

    private void Start()
    {

        // Prevent null list
        if (TimePointDelta == null) TimePointDelta = new List<TimePoint>();

        if (this.gameObject.GetComponent<Rigidbody>()) thisRigidbody = this.gameObject.GetComponent<Rigidbody>();
        else
        {

            this.gameObject.AddComponent<Rigidbody>();
            thisRigidbody = this.gameObject.GetComponent<Rigidbody>();

        }
       
    }

    private void Update()
    {

#if UNITY_EDITOR

        if (transform.hasChanged)
        {

            _currentTimePoint = new TimePoint(this.transform);
            transform.hasChanged = false;

        }

        // Time testing keys
        if (Input.GetKeyDown(KeyCode.Alpha1)) FreezeTime();
        if (Input.GetKeyDown(KeyCode.Alpha2)) UnfreezeTime();
        if (Input.GetKeyDown(KeyCode.Alpha3)) ReverseTime();
        if (Input.GetKeyDown(KeyCode.Alpha4)) ForwardTime();
        if (Input.GetKeyDown(KeyCode.Alpha5)) CurrentTimeState = TimeState.RECORDING;
        else if (Input.GetKeyUp(KeyCode.Alpha5)) CurrentTimeState = TimeState.NORMAL;

#endif
        TimeHandler();

    }

    // Handle time processes
    public void TimeHandler()
    {

        // Act based on current time state
        switch (CurrentTimeState)
        {

            case TimeState.FORWARD:
                {

                    //
                    Interpolate(1);

                    // Determine if interpolation is done
                    if (currentTimeIndex == TimePointDelta.Count - 1)
                    {

                        OnForwardComplete.Invoke();
                        FreezeTime();

                    }

                    break;

                }
            case TimeState.REVERSE:
                {

                    //
                    Interpolate(-1);

                    // Determine if interpolation is done
                    if (currentTimeIndex == 0)
                    {

                        if ((PreviousTimeState == TimeState.FROZEN ||
                            PreviousTimeState == TimeState.NORMAL) &&
                            clearAfterReverseComplete) ClearTimeHistory();
                        OnReverseComplete.Invoke();
                        FreezeTime();

                    }

                    break;

                }
            case TimeState.RECORDING:
                {

                    if (RecordAllTransform) RecordAll();
                    else RecordDeltaChange();

                    if (thisRigidbody.velocity.magnitude == 0 && TimePointDelta.Count > deltaPointLimit) CullTimeDelta();

                    break;
                }
            case TimeState.RESET:
                {

                    ResetToInitial();
                    break;

                }

        }

    }

    // Record any delta changes based on velocity change
    public void RecordDeltaChange()
    {

        if (thisRigidbody.velocity.magnitude != 0)
        {

            TimePoint newTimePoint = new TimePoint(this.transform);

            TimePointDelta.Add(newTimePoint);
            CurrentTimePoint = newTimePoint;
            if (TimePointDelta.Count != 0) currentTimeIndex = TimePointDelta.Count - 1;

        }

    }

    public void RecordAll()
    {

        TimePoint newTimePoint = new TimePoint(this.transform);

        TimePointDelta.Add(newTimePoint);
        CurrentTimePoint = newTimePoint;

        if (TimePointDelta.Count != 0) currentTimeIndex = TimePointDelta.Count - 1;

    }

    // Public callable functions for Time Object
    // Freezing and Unfreezing object, Forward and reverse
    // Start Recording changes
    // ==================================================================================
    public void FreezeTime()
    {


        TransformFreeze();
        PreviousTimeState = CurrentTimeState;
        CurrentTimeState = TimeState.FROZEN;

    }

    public void UnfreezeTime()
    {

        TransformUnfreeze();
        PreviousTimeState = CurrentTimeState;
        CurrentTimeState = TimeState.NORMAL;

    }

    public void ForwardTime()
    {

        if (TimePointDelta.Count != 0)
        {

            TransformFreeze();
            PreviousTimeState = CurrentTimeState;
            CurrentTimeState = TimeState.FORWARD;

        }

    }

    public void ReverseTime()
    {

        if (TimePointDelta.Count != 0)
        {

            TransformFreeze();
            PreviousTimeState = CurrentTimeState;
            CurrentTimeState = TimeState.REVERSE;

        }

    }
    public void ReverseTime(bool clearReverse)
    {

        if (TimePointDelta.Count != 0)
        {

            TransformFreeze();
            PreviousTimeState = CurrentTimeState;
            CurrentTimeState = TimeState.REVERSE;
            clearAfterReverseComplete = clearReverse;

        }

    }


    public void StartRecording(bool clearHistory = false, bool recordingAll = false)
    {

        if (clearHistory) ClearTimeHistory();
        RecordAllTransform = recordingAll;
        CurrentTimeState = TimeState.RECORDING;

    }

    public void EndRecording()
    {

        RecordAllTransform = false;
        CurrentTimeState = TimeState.NORMAL;

    }

    public void ClearTimeHistory()
    {

        TimePointDelta.Clear();
        CurrentTimeState = TimeState.NORMAL;

    }

    public void ResetTime()
    {

        CurrentTimeState = TimeState.RESET;

    }

    // ==================================================================================
    // Interpolate between points A -> B
    private void Interpolate(int modifier)
    {

        // Interpolation time eventually requires aggregation of transform delta
        // replacing current mas lerp t with calculated t interpolation based on
        // progression of aggregrated interpolation between points A and B

        if (interpolateTime < 0.001) interpolateTime += Time.deltaTime;
        else if (interpolateTime >= 0.001)
        {

            // Round off interpolation and remove minute transform discrepancies
            RoundOffInterpolation(TimePointDelta[currentTimeIndex]);

            interpolateTime = 0;

            if (TimePointDelta.Count != 0) currentTimeIndex += modifier;

            if (currentTimeIndex >= TimePointDelta.Count || currentTimeIndex < 0)
            {

                CurrentTimeState = TimeState.NORMAL;
                currentTimeIndex = currentTimeIndex >= TimePointDelta.Count ? TimePointDelta.Count - 1 : 0;

            }

        }

        // Lerp to point
        this.transform.position = Vector3.Lerp(this.transform.position, TimePointDelta[currentTimeIndex].thisPosition, 1);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, TimePointDelta[currentTimeIndex].thisRotation, 1);
        this.transform.localScale = Vector3.Lerp(this.transform.localScale, TimePointDelta[currentTimeIndex].thisScale, 1);

    }

    // Prevent any external forces including gravity to apply to object
    private void TransformFreeze()
    {

        thisRigidbody.isKinematic = true;
        thisRigidbody.useGravity = false;

    }

    // Allow any external forces to apply to object
    private void TransformUnfreeze()
    {

        thisRigidbody.isKinematic = false;
        thisRigidbody.useGravity = true;

    }

    // Cull time delta list to set limits
    private void CullTimeDelta()
    {

        // Cull Logic here
        // Work in Progress

        // Remove Duplicates
        TimePointDelta = TimePointDelta.Distinct().ToList();

    }

    private float resetTimer = 0;

    private void ResetToInitial()
    {

        resetTimer += Time.deltaTime;

        this.transform.position = Vector3.Lerp(this.transform.position, OriginalTimePoint.thisPosition, resetTimer);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, OriginalTimePoint.thisRotation, resetTimer);
        this.transform.localScale = Vector3.Lerp(this.transform.localScale, OriginalTimePoint.thisScale, resetTimer);

        if (resetTimer > 1)
        {

            resetTimer = 0;
            ClearTimeHistory();
            CurrentTimeState = TimeState.NORMAL;

            //
            RoundOffInterpolation(OriginalTimePoint);

        }

    }

    private void RoundOffInterpolation(TimePoint finalT)
    {

        this.transform.position = finalT.thisPosition;
        this.transform.rotation = finalT.thisRotation;
        this.transform.localScale = finalT.thisScale;

    }


    public static bool IsSimilarTimepoint(TimePoint A, TimePoint B)
    {

        bool similar = true;

        if (A.thisPosition != B.thisPosition) similar = false;
        if (A.thisRotation != B.thisRotation) similar = false;
        if (A.thisScale != B.thisScale) similar = false;
        if (A.thisTimeState != B.thisTimeState) similar = false;

        return similar;

    }

    [MenuItem("Save Time pre-recorded time data")]
    public static void SaveSerializedTimeObjectDelta(TimeObject sourceObject)
    {

        SerializedTimeDeltaChanges serializedData = new SerializedTimeDeltaChanges();
        serializedData.timeObjectDeltaChanges = sourceObject.TimePointDelta;

        //
        string dataInJSON = JsonUtility.ToJson(serializedData);

        var path = EditorUtility.SaveFilePanel(
            "Save Recording as JSON",
            "",
            ".json",
            ".json");

        if (path.Length != 0)
        {

            File.WriteAllText(path, dataInJSON);

        }

    }

    public void LoadSerializedTimeObjectDelta()
    {

        List<TimePoint> loadedDeltaPoints = new List<TimePoint>();

        var path = EditorUtility.OpenFilePanel(
            "Open JSON File", "", ".json");

        if (path.Length != 0)
        {

            string data = File.ReadAllText(path);
            SerializedTimeDeltaChanges serializedData = JsonUtility.FromJson<SerializedTimeDeltaChanges>(data);
            loadedDeltaPoints = serializedData.timeObjectDeltaChanges;
            TimePointDelta = loadedDeltaPoints;

        }

    }

}

[Serializable]
public class SerializedTimeDeltaChanges
{

    public List<TimePoint> timeObjectDeltaChanges;

}
