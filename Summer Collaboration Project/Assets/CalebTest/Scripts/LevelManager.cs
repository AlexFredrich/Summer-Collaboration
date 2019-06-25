using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Transform firstCheckpoint;

    private Transform _currentCheckpointTransform;
    public Transform CurrentCheckpointTransform
    {
        get
        {
            return _currentCheckpointTransform;
        }
        set
        {
            _currentCheckpointTransform = value;
        }
    }

    #endregion

    private void Awake()
    {
        CurrentCheckpointTransform = firstCheckpoint;
    }

    public void ChangeCurrentCheckpoint(Transform newCheckpoint)
    {
        CurrentCheckpointTransform = newCheckpoint;
    }
    
    public void RespawnPlayer()
    {
        player.transform.position = CurrentCheckpointTransform.transform.position;
        player.transform.rotation = CurrentCheckpointTransform.transform.rotation;
    }
}
