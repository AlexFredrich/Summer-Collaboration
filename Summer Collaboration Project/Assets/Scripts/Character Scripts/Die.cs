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
    private float _respawnDelayInSeconds = 3.0f;

    private LevelManager _levelManager;
    
    public bool PlayerIsDying { get; private set; }

    #endregion

    private void Start()
    {
        if (LevelManager.Instance != null)  //checkpoint script will handle instantiating the Level Manager prefab during Awake() if there is not already one in the scene */
        {
            _levelManager = LevelManager.Instance;
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

        yield return new WaitForSecondsRealtime(_respawnDelayInSeconds);

        _levelManager.RespawnPlayer();

        PlayerIsDying = false;
    }
}
