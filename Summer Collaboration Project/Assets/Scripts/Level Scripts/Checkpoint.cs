using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]

//This script goes on every checkpoint
public class Checkpoint : MonoBehaviour
{
    #region Variables

    private LevelManager _levelManager;

    private const string LEVELMANAGERPREFABNAME = "Level Manager";
    private const string PLAYERTAGNAME = "Player";

    #endregion

    private void Awake()
    {
        /* Instantiates the Level Manager prefab if one cannot be found */
        if (FindObjectOfType<LevelManager>() != null)
        {
            _levelManager = FindObjectOfType<LevelManager>().gameObject.GetComponent<LevelManager>();   //have to use FindObjectOfType because LevelManager.Instance might not be set yet during Awake and Die.Start() requires LevelManager to be instantiated during Awake
        }
        else
        {
            Instantiate(Resources.Load(LEVELMANAGERPREFABNAME), new Vector3(0, 0, 0), Quaternion.identity);

            _levelManager = FindObjectOfType<LevelManager>().gameObject.GetComponent<LevelManager>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        /* Activates the checkpoint when the player walks through it */
        if (other.gameObject.tag == PLAYERTAGNAME)
        {
            _levelManager.ChangeCurrentCheckpoint(this.gameObject.transform);
        }
    }
}
