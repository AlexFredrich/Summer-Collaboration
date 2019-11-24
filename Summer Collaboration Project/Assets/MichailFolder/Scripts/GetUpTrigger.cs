using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetUpTrigger : MonoBehaviour, IActivatable
{
    //Editor Variables
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private int rotationTargetX;
    [SerializeField] private int rotationTargetY;
    [SerializeField] private float timeTarget;

    //Private Variables
    private CharacterController characterController;
    private CameraController cameraController;
    private Animator animator;
    private Vector3 currentPlayerAngle;
    private Vector3 currentCameraAngle;
    bool canBeActivated;
    bool isActive;
    bool activate;

    void Start()
    {
        characterController = player.GetComponent<CharacterController>();
        cameraController = player.GetComponent<CameraController>();
        animator = player.GetComponentInChildren<Animator>();
        currentPlayerAngle = player.transform.eulerAngles;
        currentCameraAngle = playerCamera.transform.eulerAngles;
        canBeActivated = true;
        isActive = false;
        activate = false;
    }

    void Update()
    {
        if (activate /*&& (player.transform.eulerAngles != currentPlayerAngle || playerCamera.transform.eulerAngles != currentCameraAngle)*/)
        {
            canBeActivated = false;
            isActive = true;

            cameraController.enabled = false;

            //lerp not working correctly
            /*
            currentPlayerAngle = new Vector3(
             Mathf.LerpAngle(currentPlayerAngle.x, 0, Time.deltaTime * timeTarget),
             Mathf.LerpAngle(currentPlayerAngle.y, rotationTargetY, Time.deltaTime * timeTarget),
             Mathf.LerpAngle(currentPlayerAngle.z, 0, Time.deltaTime * timeTarget));
            player.transform.eulerAngles = currentPlayerAngle;

            currentCameraAngle = new Vector3(
             Mathf.LerpAngle(currentCameraAngle.x, 0, Time.deltaTime * timeTarget),
             Mathf.LerpAngle(currentCameraAngle.y, rotationTargetY, Time.deltaTime * timeTarget),
             Mathf.LerpAngle(currentCameraAngle.z, 0, Time.deltaTime * timeTarget));
            playerCamera.transform.eulerAngles = currentCameraAngle;
            */

            activate = false;
            animator.enabled = true;
            animator.SetTrigger("PlayNext");
        }
        else if (animator.GetBool("CanMove") && !canBeActivated && isActive)
        {
            isActive = false;
            //activate = false;
            characterController.enabled = true;
            cameraController.enabled = true;
            animator.enabled = false;
            gameObject.SetActive(false);
        }
    }

    public void Activate()
    {
        if (canBeActivated)
        {
            activate = true;
        }
    }
}
