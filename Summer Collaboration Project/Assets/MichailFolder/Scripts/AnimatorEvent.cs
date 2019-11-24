using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEvent : MonoBehaviour
{
    //Editor Variables
    [SerializeField] private string variable1Name;
    [SerializeField] private string variable2Name;

    //Private Variables
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void SetBool1True()
    {
        animator.SetBool(variable1Name, true);
    }

    void SetBool1False()
    {
        animator.SetBool(variable1Name, false);
    }
    void SetBool2True()
    {
        animator.SetBool(variable2Name, true);
    }

    void SetBool2False()
    {
        animator.SetBool(variable2Name, false);
    }
}
