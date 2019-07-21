using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]

//This script goes on the player
public class CharacterController : MonoBehaviour
{
    #region Variables

    [SerializeField]
    [Range(100, 750)]
    private int _walkSpeed = 350;
    [SerializeField]
    [Range(100, 750)]
    private int _runSpeed = 500;
    [SerializeField]
    [Range(150, 600)]
    private int _jumpHeight = 300;

    private CapsuleCollider _collider;
    private Rigidbody _rb;
    private Vector3 _movementDirection;

    private int _currentSpeed;

    public static CharacterController Instance { get; private set; }

    private const int MINRUNNINGEXTRASPEED = 150;

    //private const string HORIZONTALAXISNAME = "Horizontal";
    //private const string VERTICALAXISNAME = "Vertical";
    //private const string SPRINTBUTTONNAME = "Sprint";
    //private const string JUMPBUTTONNAME = "Jump";
    private const string WALLTAGNAME = "Wall";
    private const string GROUNDTAGNAME = "Ground";

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

        _currentSpeed = _walkSpeed;

        _collider = this.gameObject.GetComponent<CapsuleCollider>();
        _rb = this.gameObject.GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        GetMovementInput();
        ChangeSpeed();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        Jump();
    }

    private void GetMovementInput()
    {
        int verticalInput = 0;
        int horizontalInput = 0;

        /* Takes directional input if the player can move in that direction (currently not requiring the player to be on the ground so they can direct their flight in mid-air) */
        if (Input.GetKey(GameManager.Instance.MNKForwardButton))
        {
            if (CanMoveInDirection(this.gameObject.transform.forward * 1))
            {
                verticalInput = 1;
            }
        }
        else if (Input.GetKey(GameManager.Instance.MNKBackwardButton))
        {
            if (CanMoveInDirection(this.gameObject.transform.forward * -1))
            {
                verticalInput = -1;
            }
        }

        if (Input.GetKey(GameManager.Instance.MNKLeftButton))
        {
            if (CanMoveInDirection(this.gameObject.transform.right * -1))
            {
                horizontalInput = -1;
            }
        }
        else if (Input.GetKey(GameManager.Instance.MNKRightButton))
        {
            if (CanMoveInDirection(this.gameObject.transform.right * 1))
            {
                horizontalInput = 1;
            }
        }

        /* Create vector 3 for x and z velocity */
        _movementDirection = ((verticalInput * transform.forward) + (horizontalInput * transform.right)).normalized;
    }

    private void ChangeSpeed()
    {
        /* Makes the player move faster when sprint is held down and the player is on the ground */
        if (Input.GetKey(GameManager.Instance.MNKSprintButton) && IsOnGround())
        {
            _currentSpeed = _runSpeed;
        }
        else
        {
            _currentSpeed = _walkSpeed;
        }
    }

    private void MovePlayer()
    {
        /* Get current y velocity */
        Vector3 yVelocity = new Vector3(0, _rb.velocity.y, 0);

        /* Move player based on velocity from directional input */
        _rb.velocity = _movementDirection * _currentSpeed * Time.deltaTime;

        /* Add y velocity back in to desired movement direction */
        _rb.velocity += yVelocity;      //since _movementDirection only has values for x and z velocity we need to add y velocity to not constantly set vertical movement to 0 every fixed update
    }

    private void Jump()
    {
        /* Makes the player jump when the jump button is pressed and the player is on the ground */
        if (Input.GetKeyDown(GameManager.Instance.MNKJumpButton) && IsOnGround())
        {
            _rb.velocity = new Vector3(0, _jumpHeight * Time.deltaTime, 0);
        }
    }

    #region Using Unity Input Manager (old way)

    //private void GetMovementInput()
    //{
    //    float horizontalInput = 0.0f;
    //    float verticalInput = 0.0f;

    //    /* Get directional input if the player can move in that direction*/
    //    if (CanMoveInDirection(this.gameObject.transform.right * Input.GetAxisRaw(HORIZONTALAXISNAME)))     //currently not requiring the player to be on the ground so they can direct their flight in mid-air
    //    {
    //        horizontalInput = Input.GetAxisRaw(HORIZONTALAXISNAME);
    //    }

    //    if (CanMoveInDirection(this.gameObject.transform.forward * Input.GetAxisRaw(VERTICALAXISNAME)))
    //    {
    //        verticalInput = Input.GetAxisRaw(VERTICALAXISNAME);
    //    }

    //    /* Create vector 3 for x and z velocity */
    //    _movementDirection = ((horizontalInput * transform.right) + (verticalInput * transform.forward)).normalized;
    //}

    //private void ChangeSpeed()
    //{
    //    /* Makes the player move faster when sprint is held down and the player is on the ground */
    //    if (Input.GetButton(SPRINTBUTTONNAME) && IsOnGround())
    //    {
    //        _currentSpeed = _runSpeed;
    //    }
    //    else
    //    {
    //        _currentSpeed = _walkSpeed;
    //    }
    //}

    //private void MovePlayer()
    //{
    //    /* Get current y velocity */
    //    Vector3 yVelocity = new Vector3(0, _rb.velocity.y, 0);

    //    /* Move player based on velocity from directional input */
    //    _rb.velocity = _movementDirection * _currentSpeed * Time.deltaTime;

    //    /* Add y velocity back in to desired movement direction */
    //    _rb.velocity += yVelocity;      //since _movementDirection only has values for x and z velocity we need to add y velocity to not constantly set vertical movement to 0 every fixed update
    //}

    //private void Jump()
    //{
    //    /* Makes the player jump when the jump button is pressed and the player is on the ground */
    //    if (Input.GetButtonDown(JUMPBUTTONNAME) && IsOnGround())
    //    {
    //        _rb.velocity = new Vector3(0, _jumpHeight * Time.deltaTime, 0);
    //    }
    //}

    #endregion

    private bool CanMoveInDirection(Vector3 direction)
    {
        float distanceToPoints = (_collider.height / 2) - _collider.radius;

        Vector3 point1 = this.gameObject.transform.position + _collider.center + (Vector3.up * distanceToPoints);
        Vector3 point2 = this.gameObject.transform.position + _collider.center - (Vector3.up * distanceToPoints);

        float radius = _collider.radius * 0.95f;    //must be slightly smaller than the radius of the capsule so it doesn't detect the ground
        float castDistance = 0.5f;

        RaycastHit[] hits = Physics.CapsuleCastAll(point1, point2, radius, direction, castDistance);

        /* Returns false if the CapsuleCast hits an object tagged "Wall" */
        foreach (RaycastHit objectHit in hits)
        {
            if (objectHit.transform.gameObject.tag == WALLTAGNAME)   //all objects the player cannot move through must be tagged "Wall"
            {
                return false;
            }
        }

        /* Returns true if the CapsuleCast doesn't hit an object tagged "Wall" */
        return true;
    }

    private bool IsOnGround()
    {
        float distanceToPoints = (_collider.height / 2) - _collider.radius;

        Vector3 point1 = this.gameObject.transform.position + _collider.center + (Vector3.up * distanceToPoints);
        Vector3 point2 = this.gameObject.transform.position + _collider.center - (Vector3.up * distanceToPoints);
        
        float castDistance = 0.1f;

        RaycastHit[] hits = Physics.CapsuleCastAll(point1, point2, _collider.radius, -this.gameObject.transform.up, castDistance);

        /* Returns true if the CapsuleCast hits an object tagged "Ground" */
        foreach (RaycastHit objectHit in hits)
        {
            if (objectHit.transform.gameObject.tag == GROUNDTAGNAME)   //all objects the player can jump off of must be tagged "Ground"
            {
                return true;
            }
        }

        /* Returns false if the CapsuleCast doesn't hit an object tagged "Ground" */
        return false;
    }

    private void OnValidate()
    {
        /* Requires runSpeed to be faster than walkSpeed when changed in the editor */
        if (_runSpeed < _walkSpeed + MINRUNNINGEXTRASPEED)
        {
            _runSpeed = _walkSpeed + MINRUNNINGEXTRASPEED;
        }
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
