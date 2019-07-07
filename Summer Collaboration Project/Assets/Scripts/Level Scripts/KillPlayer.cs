using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script goes everything that can kill the player
public class KillPlayer : MonoBehaviour
{
    #region Variables

    private const string PLAYERTAGNAME = "Player";

    #endregion

    private void OnTriggerEnter(Collider other)
    {
        /* Kills the player when this touches them */
        if (other.gameObject.tag == PLAYERTAGNAME)
        {
            Die death = other.GetComponent<Die>();

            if (!death.PlayerIsDying)
            {
                StartCoroutine(death.KillPlayer());
            }
        }
    }
}
