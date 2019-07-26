using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]

//This script goes on an empty gameobject
public class LevelManager : MonoBehaviour
{
    #region Variables
    
    [SerializeField]
    private Transform _firstCheckpoint;
    
    private GameObject _player;
    private Transform _currentCheckpointTransform;

    public static LevelManager Instance { get; private set; }

    #endregion

    private void Awake()
    {
        /* Allows only a single instance of this script */
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _player = CharacterController.Instance.gameObject;

        /* Assigns the first checkpoint of the game */
        if (_firstCheckpoint != null)
        {
            _currentCheckpointTransform = _firstCheckpoint;   //makes the current checkpoint the first checkpoint without having to touch it
        }
        else
        {
            _currentCheckpointTransform = _player.transform;  //makes the first checkpoint the player's starting position if it wasn't set in the editor
        }
    }

    /// <summary>
    /// Changes the current checkpoint to the argument passed in.
    /// </summary>
    /// <param name="newCheckpoint"></param>
    public void ChangeCurrentCheckpoint(Transform newCheckpoint)
    {
        _currentCheckpointTransform = newCheckpoint;
    }
    
    /// <summary>
    /// Respawns the player at the current checkpoint.
    /// </summary>
    public void RespawnPlayer()
    {
        _player.transform.position = _currentCheckpointTransform.transform.position;
        _player.transform.rotation = _currentCheckpointTransform.transform.rotation;
    }

    private void OnDestroy()
    {
        /* Resets the instance to null when this is destroyed to allow for changing levels */
        if (this == Instance)
        {
            Instance = null;
        }
    }
}
