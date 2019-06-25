using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Die death = other.GetComponent<Die>();

            if (!death.PlayerIsDying)
            {
                StartCoroutine(death.KillPlayer());
            }
        }
    }
}
