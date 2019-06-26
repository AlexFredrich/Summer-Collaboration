using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script goes on an empty gameobject
public class LevelManager : MonoBehaviour
{
    #region Variables
    
    [SerializeField]
    private Transform firstCheckpoint;

    private Transform _currentCheckpointTransform;
    private GameObject player;

    #endregion

    private void Awake()
    {
        player = FindObjectOfType<CharacterController>().gameObject;
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
            _currentCheckpointTransform = player.transform;  //makes the first checkpoint the player's starting position if it wasn't set in the editor
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
        player.transform.position = _currentCheckpointTransform.transform.position;
        player.transform.rotation = _currentCheckpointTransform.transform.rotation;
    }
}
