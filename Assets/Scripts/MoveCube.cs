using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    public float speed = 2f;
    public float sensitivity = 10f;
    public Vector2 turn;
    public float minAngle = -45.0f;
    public float maxAngle = 45.0f;

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        float y = GetVerticalInput();
        Vector3 movement = new Vector3(x, y, z).normalized;
        transform.Translate(movement * speed * Time.deltaTime);
        RotateCube();
    }

    private float GetVerticalInput()
    {
        float x = 0f;

        if (Input.GetKey(KeyCode.Space))
        {
            x = speed;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            x = speed * -1;
        }

        return x;
    }

    private void RotateCube()
    {
        turn.x += Input.GetAxis("Mouse X") * sensitivity;
        turn.y += Input.GetAxis("Mouse Y") * sensitivity;

        turn.y = Mathf.Clamp(turn.y, minAngle, maxAngle);

        transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);
    }
}
