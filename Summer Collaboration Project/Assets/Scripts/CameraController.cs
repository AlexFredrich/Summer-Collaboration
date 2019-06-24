using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script goes on the Player GameObject
public class CameraController : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private float minVerticalLookAngle = -60.0f;        //x axis min clamp
    [SerializeField]
    private float maxVerticalLookAngle = 60.0f;         //x axis max clamp

    [SerializeField]
    private float verticalLookSensitivity = 5.0f;      //x axis sensitivity
    [SerializeField]
    private float horizontalLookSensitivity = 3.0f;    //y axis sensitivity

    private Camera _firstPersonCamera;

    private float _currentYRotation;
    private float _currentXRotation;

    private const string CANCELBUTTONNAME = "Cancel";

    #endregion

    private void Awake()
    {
        _currentYRotation = 0.0f;
        _currentXRotation = 0.0f;

        _firstPersonCamera = this.gameObject.GetComponentInChildren<Camera>();      //TODO: add check for whether there is actually a camera in children

        LockCursor();
    }
    
    private void Update()
    {
        GetMouseInput();
        RotatePlayerAndCamera();

        /* Unlock cursor */
        if (Input.GetButton(CANCELBUTTONNAME))
        {
            UnlockCursor();
        }
    }

    private void GetMouseInput()
    {
        /* Get mouse input */
        _currentYRotation += Input.GetAxis("Mouse X") * verticalLookSensitivity;
        _currentXRotation += Input.GetAxis("Mouse Y") * horizontalLookSensitivity;

        /* Clamp vertical look angle */
        _currentXRotation = Mathf.Clamp(_currentXRotation, minVerticalLookAngle, maxVerticalLookAngle);
    }

    private void RotatePlayerAndCamera()
    {
        /* Rotate player and camera based on mouse input */
        this.gameObject.transform.localEulerAngles = new Vector3(0, _currentYRotation, 0);
        _firstPersonCamera.transform.localEulerAngles = new Vector3(_currentXRotation, _currentYRotation, 0);   //needs to be -_currentXRotation if MouseY is not set to inverse in project settings
    }

    //TODO: move these to UI script later on
    private void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void UnlockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
