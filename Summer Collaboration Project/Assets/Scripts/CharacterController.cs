using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

//This script goes on the Player GameObject
public class CharacterController : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private int walkSpeed = 250;

    private Rigidbody _rb;
    private Vector3 _movementDirection;

    #endregion

    private void Awake()
    {
        _rb = this.gameObject.GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        GetMovementInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void GetMovementInput()
    {
        /* Get directional input */
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        /* Create vector 3 for x and z velocity */
        _movementDirection = (horizontalInput * transform.right + verticalInput * transform.forward).normalized;
    }

    private void MovePlayer()
    {
        /* Get current y velocity */
        Vector3 yVelocity = new Vector3(0, _rb.velocity.y, 0);

        /* Move player based on velocity from directional input */
        _rb.velocity = _movementDirection * walkSpeed * Time.deltaTime;

        /* Add y velocity back in */
        _rb.velocity += yVelocity;      //since _movementDirection only has values for x and z velocity we need to add y velocity to not constantly set vertical movement to 0 every fixed update
    }
}
