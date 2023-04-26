using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFloorOffset : MonoBehaviour
{

    public Transform target;
    public float offset = 1f;

    // Update is called once per frame
    void Update()
    {

        OffsetFromFloor();
    }

    private void OffsetFromFloor()
    {
        if (transform.position.y <= target.position.y + offset)
        {
            var z = target.position.z + offset;
            transform.position = new Vector3(transform.position.x, z, transform.position.z);
        }
    }
}
