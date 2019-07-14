using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]

//This script goes on an empty gameobject
public class LevelManager : MonoBehaviour
{
    #region Variables
    
    [SerializeField]
    private Transform firstCheckpoint;

    private Transform _currentCheckpointTransform;
    private GameObject _player;

    private static LevelManager _instance;
    public static LevelManager Instance
    {
        get
        {
            return _instance;
        }
    }

    #endregion

    private void Awake()
    {
        /* Allows only a single instance of this script */
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        /* Assigns the first checkpoint of the game */
        if (firstCheckpoint != null)
        {
            _currentCheckpointTransform = firstCheckpoint;   //makes the current checkpoint the first checkpoint without having to touch it
        }
        else
        {
            _currentCheckpointTransform = _player.transform;  //makes the first checkpoint the player's starting position if it wasn't set in the editor
        }

        _player = CharacterController.Instance.gameObject;
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
        if (this == _instance)
        {
            _instance = null;
        }
    }
}
