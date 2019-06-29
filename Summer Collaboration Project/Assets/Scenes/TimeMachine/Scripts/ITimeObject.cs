using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimeObject
{

    void UnfreezeTime();

    void FreezeTime();

    void ForwardTime();

    void ReverseTime();

    void ClearTimeHistory();

    TimeState CurrentTimeState { get; }

    TimeState PreviousTimeState { get; }

    List<TimePoint> TimePointDelta { get; }

}
