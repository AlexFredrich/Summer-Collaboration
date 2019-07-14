using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]

//This script goes on the player
public class Die : MonoBehaviour
{
    #region Variables

    [SerializeField]
    [Range(0.0f, 10.0f)]
    private float respawnDelayInSeconds = 3.0f;

    private LevelManager _levelManager;

    private bool _playerIsDying;
    public bool PlayerIsDying
    {
        get
        {
            return _playerIsDying;
        }
        private set
        {
            _playerIsDying = value;
        }
    }

    #endregion

    private void Start()
    {
        //* Checkpoint script will instantiate the Level Manager prefab during Awake() if there is not already one in the scene */
        if (FindObjectOfType<LevelManager>() != null)
        {
            _levelManager = FindObjectOfType<LevelManager>().gameObject.GetComponent<LevelManager>();
        }
    }

    /// <summary>
    /// Starts the player respawn process.
    /// </summary>
    /// <returns></returns>
    public IEnumerator KillPlayer()
    {
        PlayerIsDying = true;

        //TODO: death screen or whatever

        yield return new WaitForSecondsRealtime(respawnDelayInSeconds);

        _levelManager.RespawnPlayer();

        PlayerIsDying = false;
    }
}
