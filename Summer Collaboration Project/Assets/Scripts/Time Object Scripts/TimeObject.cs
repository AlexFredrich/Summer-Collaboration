using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class TimeObject : MonoBehaviour, ITimeObject
{

    // How much time points should be recorded before culling starts
    [SerializeField]
    private int deltaPointLimit = 200;

    [SerializeField]
    private TimeState _previousTimeState;
    public TimeState PreviousTimeState { get => _previousTimeState; private set => _previousTimeState = value; }

    // What is our current time state
    [SerializeField]
    private TimeState _currentTimeState;
    public TimeState CurrentTimeState { get => _currentTimeState; private set => _currentTimeState = value; }

    // What is our current time point
    [SerializeField]
    private TimePoint _currentTimePoint;
    public TimePoint CurrentTimePoint { get => _currentTimePoint; private set => _currentTimePoint = value; }

    // Records Initial Timepoint since game started
    [SerializeField]
    private TimePoint _originalTimePoint;
    public TimePoint OriginalTimePoint { get => _originalTimePoint; private set => _originalTimePoint = value; }

    // List of time point delta changes
    [SerializeField]
    private List<TimePoint> _timePointDelta;
    public List<TimePoint> TimePointDelta { get => _timePointDelta; private set => _timePointDelta = value; }

    // Rigidbody reference to parent GameObject
    private Rigidbody thisRigidbody;

    // Interpolation Time
    private float interpolateTime = 0;

    // Track the current index of the TimePointDelta;
    private int currentTimeIndex;

    // Allow other gameobjects to listen into time events
    public UnityEvent OnReverseComplete;
    public UnityEvent OnForwardComplete;

    private void Awake()
    {

        // save initial time point from the moment the game starts
        OriginalTimePoint = new TimePoint(this.gameObject.transform);

    }

    private void Start()
    {

        // Prevent null list
        TimePointDelta = new List<TimePoint>();

        if (this.gameObject.GetComponent<Rigidbody>()) thisRigidbody = this.gameObject.GetComponent<Rigidbody>();
        else
        {

            this.gameObject.AddComponent<Rigidbody>();
            thisRigidbody = this.gameObject.GetComponent<Rigidbody>();

        }
       
    }

    private void Update()
    {

#if UNITY_DEBUG
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

                        if (PreviousTimeState == TimeState.FROZEN ||
                            PreviousTimeState == TimeState.NORMAL) ClearTimeHistory();
                        OnReverseComplete.Invoke();
                        FreezeTime();

                    }

                    break;

                }
            case TimeState.RECORDING:
                {

                    RecordDeltaChange();
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

    public void StartRecording(bool clearHistory = false)
    {

        if (clearHistory) ClearTimeHistory();
        CurrentTimeState = TimeState.RECORDING;

    }

    public void EndRecording()
    {

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

}
