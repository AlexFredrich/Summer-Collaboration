using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchFaceScript : MonoBehaviour
{
    public GameObject watchPivot;


    public void WatchPointNorth()
    {
        watchPivot.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void WatchPointEast()
    {
        watchPivot.transform.rotation = Quaternion.Euler(0, 90, 0);
    }

    public void WatchPointSouth()
    {
        watchPivot.transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    public void WatchPointWest()
    {
        watchPivot.transform.rotation = Quaternion.Euler(0, 270, 0);
    }
}
