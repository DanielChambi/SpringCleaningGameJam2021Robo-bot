using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGPlayer : MonoBehaviour
{
    public float moveSpeed = 10;
    public float gravity = 20;

    //distance travelled that counts as a step
    public float stepSize = 2f;
    //counter for distance travelled between steps
    float stepDistance = 0;

    CharacterController characterController;

    public RPGOverWorldController overWorldController;

    //Initial position on scene load
    Vector3 startPos;

    //direction vector por characte rcontroller movement
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

        //get direction vector on local space
        Vector3 flatMovement = transform.TransformDirection(inputDirection) * moveSpeed * Time.deltaTime;

        //add direction vector to previous vertical velocity
        moveDirection = new Vector3(flatMovement.x, moveDirection.y, flatMovement.z);

        moveDirection.y -= gravity * Time.deltaTime;

        //apply movment through character controller component
        characterController.Move(moveDirection);

        //Calculate step distance travelled
        Vector2 pos = new Vector2(transform.position.x, transform.position.z);
        Vector2 prevPos = new Vector2(prevPosition.x, prevPosition.z);

        stepDistance += Vector2.Distance(pos, prevPos);

        if(stepDistance >= stepSize)
        {
            stepDistance = 0;

            //inform controller of a step taken
            int t = overWorldController.PlayerSteps();
            Debug.Log(t);
        }

        prevPosition = transform.position;
    }


    /*Set starting position on scene load
     * 
     */
    public void SetStartPosition(Vector3 vector)
    {
        startPos = vector;
    }
}
