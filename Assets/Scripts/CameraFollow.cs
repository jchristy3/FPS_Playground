using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float smoothTime = 0.75f;
    public float sensitivity = 1.0f;
    Vector3 currentVelocity;
    public float distance = 3;
    public float vertDistance = 1;

    void LateUpdate()
    {
        //transform.position = target.position + offset;
        //transform.localRotation = target.localRotation;
        Vector3 target = player.position + (transform.position - player.position).normalized * distance;
        target.y = player.position.y + vertDistance;
        
        transform.position = Vector3.SmoothDamp(transform.position, target, ref currentVelocity, smoothTime);
        
        transform.LookAt(player);
        transform.RotateAround(player.position, Vector3.up, Input.GetAxis("Mouse X")*sensitivity);
        transform.RotateAround(player.position, transform.right, Input.GetAxis("Mouse Y")*sensitivity);
    }
}
