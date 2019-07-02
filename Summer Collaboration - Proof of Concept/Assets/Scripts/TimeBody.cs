using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    //[Tooltip("How far back in time the object can be reversed (in seconds). Negative values for infinite time.")]
    //[SerializeField] private float recordTime = 10f;

    private float recordTime;
    private bool stayFrozen;
    private bool isRewinding = false;
    List<PointInTime> pointsInTime;
    Rigidbody rb;
    PowerRewind pRewind;

    void Start()
    {
        pointsInTime = new List<PointInTime>();
        rb = GetComponent<Rigidbody>();
        pRewind = GameObject.FindGameObjectWithTag("Player").GetComponent<PowerRewind>();

        recordTime = pRewind.RewindTime;
        stayFrozen = pRewind.StayFrozen;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            StartRewind();
        }
        if (Input.GetMouseButtonUp(1))
        {
            StopRewind();
        }
    }

    void FixedUpdate()
    {
        if (isRewinding)
        {
            Rewind();
        }
        else
        {
            Record();
        }
    }

    void Rewind()
    {
        if (pointsInTime.Count > 0)
        {
            PointInTime pointInTime = pointsInTime[0];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            pointsInTime.RemoveAt(0);
        }
        else
        {
            if (!stayFrozen)
            {
                StopRewind();
            }
        }
    }

    void Record()
    {
        if (recordTime >= 0 && pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }

        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
    }

    public void StartRewind()
    {
        isRewinding = true;
        rb.isKinematic = true;
    }

    public void StopRewind()
    {
        isRewinding = false;
        rb.isKinematic = false;
    }
}
