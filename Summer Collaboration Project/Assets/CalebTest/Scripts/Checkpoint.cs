using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    #region Variables

    private LevelManager _levelManager;

    private const string LEVELMANAGERPREFABNAME = "Level Manager";

    #endregion

    private void Awake()
    {
        /* Instantiates the Level Manager prefab if one cannot be found */
        if (FindObjectOfType<LevelManager>())
        {
            _levelManager = FindObjectOfType<LevelManager>().GetComponent<LevelManager>();
        }
        else
        {
            Instantiate(Resources.Load(LEVELMANAGERPREFABNAME), new Vector3(0, 0, 0), Quaternion.identity);

            _levelManager = FindObjectOfType<LevelManager>().GetComponent<LevelManager>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _levelManager.CurrentCheckpointTransform = this.gameObject.transform;
        }
    }
}
