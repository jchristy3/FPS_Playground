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
    public float gravity = 15f;
    public float maxHeight = 10f;

    public Transform cameraTransform;

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal") * strafeSpeed;
        float z = GetForwardInput();

        float y = GetVerticalInput();
        Vector3 movement = new Vector3(x, -gravity*Time.deltaTime, z);
        //var flatDirection = Vector3.ProjectOnPlane(transform.TransformDirection(movement), Vector3.up);
        //var newPosition = playerRigidbody.position + flatDirection;
        //playerRigidbody.MovePosition(newPosition);
        var moveDirection = cameraTransform.TransformDirection(movement);
        moveDirection = moveDirection * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + moveDirection);
        RotatePlayer();

        Jump();
        FloorDetection();
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

    private void RotatePlayer()
    {
        turn.x += Input.GetAxis("Mouse X") * sensitivity;
        //turn.y += Input.GetAxis("Mouse Y") * sensitivity;

        //turn.y = Mathf.Clamp(turn.y, minAngle, maxAngle);

        var rotation = Quaternion.Euler(0, turn.x, 0);
        playerRigidbody.MoveRotation(rotation);
    }

    private void Jump()
    {
        if (Input.GetButton("Jump") && transform.position.y < maxHeight)
        {
            playerRigidbody.AddForce(transform.up * jumpForce);
        }
    }

    private void FloorDetection()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(playerRigidbody.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(playerRigidbody.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(playerRigidbody.position, transform.TransformDirection(Vector3.down) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }
}
