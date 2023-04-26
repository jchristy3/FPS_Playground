using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float forwardSpeed = 10f;
    public float backwardSpeed = 10f;
    public float strafeSpeed = 10f;
    public float upSpeed = 10f;
    public float downSpeed = 2f;

    public float sensitivity = 10f;
    public Vector2 turn;
    public float minAngle = -45.0f;
    public float maxAngle = 45.0f;

    public Rigidbody playerRigidbody;
    public float jumpForce = 20f;

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal") * strafeSpeed;
        float z = GetForwardInput();

        float y = GetVerticalInput();
        Vector3 movement = new Vector3(x, 0, z);
        var flatDirection = Vector3.ProjectOnPlane(transform.TransformDirection(movement), Vector3.up);
        var newPosition = playerRigidbody.position + flatDirection;
        playerRigidbody.MovePosition(newPosition);
        RotateCube();

        Jump();
    }

    private float GetVerticalInput()
    {
        float x = 0f;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            x = upSpeed;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            x = downSpeed * -1;
        }

        return x;
    }

    private float GetForwardInput()
    {
        float result = 0f;

        float x = Input.GetAxisRaw("Vertical");

        if(x > 0)
        {
            result = x * forwardSpeed;
        }
        else if (x < 0)
        {
            result = x * backwardSpeed;
        }

        return result;
    }

    private void RotateCube()
    {
        turn.x += Input.GetAxis("Mouse X") * sensitivity;
        turn.y += Input.GetAxis("Mouse Y") * sensitivity;

        turn.y = Mathf.Clamp(turn.y, minAngle, maxAngle);

        var rotation = Quaternion.Euler(-turn.y, turn.x, 0);
        playerRigidbody.MoveRotation(rotation);
    }

    private void Jump()
    {
        if (Input.GetButton("Jump"))
        {
            playerRigidbody.AddForce(transform.up * jumpForce);
        }
    }
}
