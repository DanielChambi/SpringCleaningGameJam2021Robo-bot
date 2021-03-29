using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGPlayer : MonoBehaviour
{
    public float moveSpeed = 10;
    public float gravity = 20;
    public float stepSize = 2f;

    float stepDistance = 0;

    CharacterController characterController;
    public RPGOverWorldController overWorldController;

    Vector3 startPos;
    Vector3 moveDirection;

    Vector3 prevPosition;

    float x_axis;
    float y_axis;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        transform.position = RPGOverWorldController.playerPos;

        if(startPos != Vector3.zero)
        {
            characterController.enabled = false;
            transform.position = startPos;
            characterController.enabled = true;
        }
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

        Vector3 flatMovement = transform.TransformDirection(inputDirection) * moveSpeed * Time.deltaTime;

        moveDirection = new Vector3(flatMovement.x, moveDirection.y, flatMovement.z);

        moveDirection.y -= gravity * Time.deltaTime;

        characterController.Move(moveDirection);

        //Calculate step
        Vector2 pos = new Vector2(transform.position.x, transform.position.z);
        Vector2 prevPos = new Vector2(prevPosition.x, prevPosition.z);

        stepDistance += Vector2.Distance(pos, prevPos);

        if(stepDistance >= stepSize)
        {
            stepDistance = 0;

            int t =overWorldController.PlayerSteps();
            Debug.Log(t);
        }

        prevPosition = transform.position;
    }

    public void SetStartPosition(Vector3 vector)
    {
        startPos = vector;
    }
}
