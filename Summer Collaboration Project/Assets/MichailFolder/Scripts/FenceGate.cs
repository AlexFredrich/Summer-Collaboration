using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceGate : MonoBehaviour, IActivatable
{
    //Private Variables
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Activate()
    {
        animator.SetBool("IsOpen", true);
    }
}
