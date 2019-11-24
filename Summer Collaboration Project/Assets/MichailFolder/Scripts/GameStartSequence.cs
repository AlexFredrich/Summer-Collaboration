using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartSequence : MonoBehaviour
{
    //Editor Variables
    [SerializeField] private GameObject playerController;
    [SerializeField] private bool skipStartSequence;

    //Private Variables
    private CharacterController characterController;
    private CameraController cameraController;
    private Animator animator;

    private bool canLook = false;
    
    void Start()
    {
        characterController = playerController.GetComponent<CharacterController>();
        cameraController = playerController.GetComponent<CameraController>();
        animator = playerController.GetComponentInChildren<Animator>();

        if (!skipStartSequence)
        {
            characterController.enabled = false;
            cameraController.enabled = false;
        }
        else
        {
            animator.SetBool("CanLook", true);
            animator.SetBool("CanMove", true);
            canLook = true;
            animator.gameObject.transform.localPosition = new Vector3(0, 0.8f, 0);
            characterController.enabled = true;
            cameraController.enabled = true;
            animator.enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (animator.GetBool("CanLook") && !canLook)
        {
            canLook = true;
            cameraController.enabled = true;
            animator.enabled = false;
        }
    }
}
