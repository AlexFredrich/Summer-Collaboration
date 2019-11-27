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
    private bool _needToJump;

    public static CharacterController Instance { get; private set; }

    private const int MINRUNNINGEXTRASPEED = 150;

    private const string CONTROLLERHORIZONTALAXISNAME = "Controller Horizontal";
    private const string CONTROLLERVERTICALAXISNAME = "Controller Vertical";
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
        _needToJump = false;

        _collider = this.gameObject.GetComponent<CapsuleCollider>();
        _rb = this.gameObject.GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        GetMovementInput();
        ChangeSpeed();
        GetJumpInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        Jump();
    }

    private void GetMovementInput()
    {
        float verticalInput = 0.0f;
        float horizontalInput = 0.0f;

        /* Takes directional input if the player can move in that direction (currently not requiring the player to be on the ground so they can direct their flight in mid-air) */
        if (Input.GetKey(GameManager.Instance.ForwardButton))
        {
            if (CanMoveInDirection(this.gameObject.transform.forward * 1))
            {
                verticalInput = 1.0f;
            }
        }
        else if (Input.GetKey(GameManager.Instance.BackwardButton))
        {
            if (CanMoveInDirection(this.gameObject.transform.forward * -1))
            {
                verticalInput = -1.0f;
            }
        }

        if (Input.GetKey(GameManager.Instance.LeftButton))
        {
            if (CanMoveInDirection(this.gameObject.transform.right * -1))
            {
                horizontalInput = -1.0f;
            }
        }
        else if (Input.GetKey(GameManager.Instance.RightButton))
        {
            if (CanMoveInDirection(this.gameObject.transform.right * 1))
            {
                horizontalInput = 1.0f;
            }
        }

        /* Looks for controller input if there is no keyboard input */
        if (verticalInput == 0 && horizontalInput == 0)
        {
            if (CanMoveInDirection(this.gameObject.transform.right * Input.GetAxisRaw(CONTROLLERHORIZONTALAXISNAME)))
            {
                horizontalInput = Input.GetAxisRaw(CONTROLLERHORIZONTALAXISNAME);
            }

            if (CanMoveInDirection(this.gameObject.transform.forward * Input.GetAxisRaw(CONTROLLERVERTICALAXISNAME)))
            {
                verticalInput = Input.GetAxisRaw(CONTROLLERVERTICALAXISNAME);
            }
        }

        /* Create vector 3 for x and z velocity */
        _movementDirection = ((verticalInput * transform.forward) + (horizontalInput * transform.right)).normalized;
    }

    private void ChangeSpeed()
    {
        /* Makes the player move faster when sprint is held down and the player is on the ground */
        if (Input.GetKey(GameManager.Instance.SprintButton) && IsOnGround())
        {
            _currentSpeed = _runSpeed;
        }
        else if (IsOnGround())
        {
            _currentSpeed = _walkSpeed;     //speed is maintained while in mid-air regardless of whether sprint is held down
        }
    }

    private void GetJumpInput()
    {
        /* Makes the player jump when the jump button is pressed and the player is on the ground */
        if (Input.GetKeyDown(GameManager.Instance.JumpButton) && IsOnGround())
        {
            _needToJump = true;
        }
    }

    private void MovePlayer()
    {
        /* Get current y velocity */
        Vector3 yVelocity = new Vector3(0, _rb.velocity.y, 0);

        /* Move player based on velocity from directional input */
        _rb.velocity = _movementDirection * _currentSpeed * Time.fixedDeltaTime;

        /* Add y velocity back in to desired movement direction */
        _rb.velocity += yVelocity;      //since _movementDirection only has values for x and z velocity, we need to add y velocity to not constantly set vertical movement to 0 every fixed update
    }

    private void Jump()
    {
        /* Adds jump force if the player gave jump input */
        if (_needToJump)
        {
            _needToJump = false;

            _rb.velocity += new Vector3(0, _jumpHeight * Time.fixedDeltaTime, 0);
        }
    }

    private bool CanMoveInDirection(Vector3 direction)
    {
        /* Returns false if the SweepTest hits an object while in mid-air so the player doesn't get stuck on the side of them */
        if (!IsOnGround())
        {
            Vector3 currentHorizontalVelocity = _rb.velocity;

            currentHorizontalVelocity.y = 0;

            float moveDistance = currentHorizontalVelocity.magnitude * Time.fixedDeltaTime;

            RaycastHit hit;

            if (_rb.SweepTest(direction, out hit, moveDistance))
            {
                //_rb.velocity = new Vector3(0, _rb.velocity.y, 0);

                return false;
            }
        }

        /* Returns true if the player is on the ground or the SweepTest doesn't hit an object while in mid-air */
        return true;

        //***commented out to use SweepTest instead of CapsuleCast.***
        //float distanceToPoints = (_collider.height / 2) - _collider.radius;

        //Vector3 point1 = this.gameObject.transform.position + _collider.center + (Vector3.up * distanceToPoints);
        //Vector3 point2 = this.gameObject.transform.position + _collider.center - (Vector3.up * distanceToPoints);

        //float radius = _collider.radius * 0.95f;    //must be slightly smaller than the radius of the capsule so it doesn't detect the ground
        //float castDistance = 0.5f;

        //RaycastHit[] hits = Physics.CapsuleCastAll(point1, point2, radius, direction, castDistance);

        ///* Returns false if the CapsuleCast hits an object tagged "Wall" while in mid-air so the player doesn't get stuck on the side of them */
        //if (!IsOnGround())
        //{
        //    foreach (RaycastHit objectHit in hits)
        //    {
        //        if (objectHit.transform.gameObject.tag == WALLTAGNAME)   //READ: all objects the player cannot move through while jumping must be tagged "Wall"
        //        {
        //            return false;
        //        }
        //    }
        //}

        ///* Returns true if the player is on the ground or the CapsuleCast doesn't hit an object tagged "Wall" while in mid-air */
        //return true;
    }

    private bool IsOnGround()
    {
        float distanceToPoints = (_collider.height / 2) - _collider.radius;

        Vector3 point1 = this.gameObject.transform.position + _collider.center + (Vector3.up * distanceToPoints);
        Vector3 point2 = this.gameObject.transform.position + _collider.center - (Vector3.up * distanceToPoints);

        float radius = _collider.radius * 0.95f;    //must be slightly smaller than the radius of the capsule so it doesn't detect objects the sides of the player are touching
        float castDistance = 0.1f;

        RaycastHit[] hits = Physics.CapsuleCastAll(point1, point2, radius, -this.gameObject.transform.up, castDistance);

        //***commented out since it would be unnecessary work to tag all objects the player can jump off of. should check for objects the player can't jump off of instead.***
        ///* Returns true if the CapsuleCast hits an object tagged "Ground" */
        //foreach (RaycastHit objectHit in hits)
        //{
        //    if (objectHit.transform.gameObject.tag == GROUNDTAGNAME)   //READ: all objects the player can jump off of must be tagged "Ground"
        //    {
        //        return true;
        //    }
        //}
        
        /* Returns true if the CapsuleCast hits an object below the player that will not kill the player */
        foreach (RaycastHit objectHit in hits)
        {
            if (hits.Length > 1 && objectHit.transform.gameObject.GetComponent<KillPlayer>() == null)
            {
                return true;
            }
        }

        /* Returns false if the CapsuleCast doesn't hit a safe object below the player */
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
