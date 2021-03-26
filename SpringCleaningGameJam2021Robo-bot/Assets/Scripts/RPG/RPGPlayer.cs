using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGPlayer : MonoBehaviour
{
    public float moveSpeed = 10;
    public float gravity = 20;

    CharacterController characterController;

    Vector3 moveDirection;

    float x_axis;
    float y_axis;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //update velocity
        x_axis = Input.GetAxis("Horizontal");
        y_axis = Input.GetAxis("Vertical");

        if (!characterController.isGrounded)
        {
            x_axis = 0;
            y_axis = 0;
        }

        Vector3 inputDirection = new Vector3(x_axis, 0, y_axis);
        Vector3 transformDirection = transform.TransformDirection(inputDirection);

        Vector3 flatMovement = transform.TransformDirection(inputDirection) * moveSpeed * Time.deltaTime;

        moveDirection = new Vector3(flatMovement.x, moveDirection.y, flatMovement.z);

        moveDirection.y -= gravity * Time.deltaTime;

        characterController.Move(moveDirection);
    }
}
