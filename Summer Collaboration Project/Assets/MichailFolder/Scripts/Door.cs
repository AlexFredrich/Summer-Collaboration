using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IActivatable
{
    //Private Variables
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Activate()
    {
        Debug.Log("Activate Hit");
        if (!animator.GetBool("IsOpen"))
        {
            Debug.Log("IsOpen set to true");
            animator.SetBool("IsOpen", true);
        }
    }
}
