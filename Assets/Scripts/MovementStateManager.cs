using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    #region Movement
    CharacterController controller;
    [Header("Movement")]
    [SerializeField] float moveSpeed = 3f;
    private Vector3 direction;
    private float vInput, hInput;
    #endregion
    #region GroundCheck
    [Header("GroundCheck")]
    [SerializeField] float groundYOffset=0.6f;//Adjusted according to player height
    [SerializeField] LayerMask groundMask;
    private Vector3 spherePos;

    [SerializeField] float gravity = -9.81f;
    private Vector3 velocity;
    #endregion
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        GetDirAndMove();
        Gravity();
    }
    void GetDirAndMove()
    {
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        direction = transform.forward * vInput + transform.right * hInput;
        controller.Move(direction.normalized * moveSpeed * Time.deltaTime);
    }
    //We should check player is grounded or not for add gravity
    bool IsGrounded()
    {
        //Calculated a position slightly below the player position with groundYOffset
        spherePos = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);
        //With Physics.CheckSphere, create a sphere in this position and check the contact or not with the selected layermask.
        //Don't forget change player tag from default otherwise it won't work
        if (Physics.CheckSphere(spherePos, controller.radius - 0.05f, groundMask)) return true;
        return false;
    }
    //We have to apply gravity to player because unity build-in-character-controller does not apply gravity
    void Gravity()
    {
        if (!IsGrounded()) velocity.y += gravity * Time.deltaTime;
        else if (velocity.y < 0) velocity.y = -2;//We give extra force when we hit the ground because to always touch the ground and not float
        
        controller.Move(velocity * Time.deltaTime);
    }
    //Draws the sphere position in the scene view as a wire when the player is selected
    //In editor gives NullReferenceException or UnassignedReferenceException error because spherePos not initialized.Can close after adjustment
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spherePos, controller.radius - 0.05f);
    }
}
