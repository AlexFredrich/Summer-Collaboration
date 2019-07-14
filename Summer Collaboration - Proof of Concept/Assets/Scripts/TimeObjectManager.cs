using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeObjectManager : MonoBehaviour
{

    [SerializeField]
    private List<ITimeObject> _frozenObjects;
    public List<ITimeObject> FrozenObjects { get => _frozenObjects; private set => _frozenObjects = value; }

    [SerializeField]
    private List<ITimeObject> _recordedObjects;
    public List<ITimeObject> RecordedObjects { get => _recordedObjects; private set => _recordedObjects = value; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
