using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script goes on the Player GameObject
public class CameraController : MonoBehaviour
{
    #region Variables

    [SerializeField]
    [Range(10, 170)]
    private int verticalLookAngleRange = 120;

    [SerializeField]
    [Range(0.0f, 10.0f)]
    private float verticalLookSensitivity = 5.0f;       //x axis sensitivity
    [SerializeField]
    [Range(0.0f, 10.0f)]
    private float horizontalLookSensitivity = 3.0f;     //y axis sensitivity

    private Camera _firstPersonCamera;

    [Range(-85.0f, 0.0f)]
    private float _minVerticalLookAngle;     //x axis min clamp
    [Range(0.0f, 85.0f)]
    private float _maxVerticalLookAngle;     //x axis max clamp

    private float _currentYRotation;
    private float _currentXRotation;

    private bool _cursorIsLocked;

    private const string MOUSEXAXISNAME = "Mouse X";
    private const string MOUSEYAXISNAME = "Mouse Y";
    private const string CANCELBUTTONNAME = "Cancel";

    #endregion

    private void Awake()
    {
        _currentYRotation = 0.0f;
        _currentXRotation = 0.0f;

        _minVerticalLookAngle = -(verticalLookAngleRange / 2);
        _maxVerticalLookAngle = verticalLookAngleRange / 2;

        _firstPersonCamera = this.gameObject.GetComponentInChildren<Camera>();      //TODO: add check for whether there is actually a camera in children
        _cursorIsLocked = false;

        /* Lock cursor (move to UI script eventually) */
        ChangeCursorLock();
    }
    
    private void Update()
    {
        GetMouseInput();
        RotatePlayer();
        RotateCamera();

        /* Unlock cursor (move to UI script eventually) */
        if (Input.GetButton(CANCELBUTTONNAME))
        {
            ChangeCursorLock();
        }
    }

    private void GetMouseInput()
    {
        /* Get mouse input */
        _currentYRotation += Input.GetAxis(MOUSEXAXISNAME) * verticalLookSensitivity;
        _currentXRotation += Input.GetAxis(MOUSEYAXISNAME) * horizontalLookSensitivity;

        /* Clamp vertical look angle */
        _currentXRotation = Mathf.Clamp(_currentXRotation, _minVerticalLookAngle, _maxVerticalLookAngle);
    }

    private void RotatePlayer()
    {
        /* Rotate player based on mouse horizontal input */
        this.gameObject.transform.localEulerAngles = new Vector3(0, _currentYRotation, 0);
    }

    private void RotateCamera()
    {
        /* Rotate camera based on mouse vertical input */
        _firstPersonCamera.transform.localEulerAngles = new Vector3(_currentXRotation, 0, 0);   //MouseY needs to be set to inverse in project settings
    }

    //TODO: move this to UI script later on
    private void ChangeCursorLock()
    {
        if (_cursorIsLocked)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            _cursorIsLocked = false;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            _cursorIsLocked = true;
        }
    }
}
