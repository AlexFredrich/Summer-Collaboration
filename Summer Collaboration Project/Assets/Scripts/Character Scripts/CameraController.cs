using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]

//This script goes on the player
public class CameraController : MonoBehaviour
{
    #region Variables

    [SerializeField]
    [Range(10, 170)]
    private int _verticalLookAngleRange = 120;

    [SerializeField]
    [Range(0.0f, 10.0f)]
    private float _verticalLookSensitivity = 5.0f;       //x axis sensitivity
    [SerializeField]
    [Range(0.0f, 10.0f)]
    private float _horizontalLookSensitivity = 3.0f;     //y axis sensitivity

    private Camera _firstPersonCamera;

    [Range(-85.0f, 0.0f)]
    private float _minVerticalLookAngle;     //x axis min clamp
    [Range(0.0f, 85.0f)]
    private float _maxVerticalLookAngle;     //x axis max clamp

    private float _yRotation;
    private float _xRotation;

    public static CameraController Instance { get; private set; }

    private const string LOOKXAXISNAME = "Look X";
    private const string LOOKYAXISNAME = "Look Y";
    private const string FIRSTPERSONCAMERANAME = "Main Camera";

    #endregion

    private void Awake()
    {
        /* Allows only a single instance of this script */
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        _yRotation = 0.0f;
        _xRotation = 0.0f;

        _minVerticalLookAngle = -(_verticalLookAngleRange / 2);
        _maxVerticalLookAngle = _verticalLookAngleRange / 2;

        /* Adds a new child GameObject with a Camera component and AudioListener if one cannot be found */
        if (this.gameObject.transform.Find(FIRSTPERSONCAMERANAME) != null)
        {
            _firstPersonCamera = this.gameObject.transform.Find(FIRSTPERSONCAMERANAME).gameObject.GetComponent<Camera>();
        }
        else
        {
            GameObject newCamera = new GameObject(FIRSTPERSONCAMERANAME);

            newCamera.AddComponent<Camera>();

            if (FindObjectOfType<AudioListener>() == null)
            {
                newCamera.AddComponent<AudioListener>();
            }
            
            newCamera.transform.parent = this.gameObject.transform;
            newCamera.transform.localPosition = new Vector3(0, 0.5f, 0);
            newCamera.transform.localRotation = Quaternion.identity;

            _firstPersonCamera = newCamera.GetComponent<Camera>();
        }
    }

    private void Update()
    {
        GetLookInput();
        RotatePlayerHorizontally();
        RotateCameraVertically();
    }

    private void GetLookInput()
    {
        /* Get mouse input */
        _yRotation = Input.GetAxis(LOOKXAXISNAME) * _verticalLookSensitivity;
        _xRotation = Input.GetAxis(LOOKYAXISNAME) * _horizontalLookSensitivity;

        /* Clamp vertical look angle */
        _xRotation = Mathf.Clamp(_xRotation, _minVerticalLookAngle, _maxVerticalLookAngle);
    }

    private void RotatePlayerHorizontally()
    {
        /* Rotate player based on horizontal input */
        this.gameObject.transform.Rotate(0, _yRotation, 0);
    }

    private void RotateCameraVertically()
    {
        /* Rotate camera based on vertical input */
        _firstPersonCamera.transform.Rotate(_xRotation, 0, 0);      //LookY for the mouse (not the controller) needs to be set to inverse in project settings
    }

    private void OnDestroy()
    {
        /* Resets the instance to null when this is destroyed to allow for respawning/changing levels */
        if (this == Instance)
        {
            Instance = null;
        }
    }
}
