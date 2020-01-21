using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform lookAt;
    public float chaseSpeed = 2.5f;

    private Vector3 moveSpeed;
    private Vector3 startOffset;

    // Start is called before the first frame update
    void Start()
    {
        startOffset = transform.position - lookAt.position;
    }

    // Update is called once per frame
    void Update()
    {
        moveSpeed = lookAt.position + startOffset;

        // Y Limit high
        //moveSpeed.y = Mathf.Clamp(moveSpeed.y, -5, 50);

        moveSpeed.x = Mathf.Lerp(transform.position.x, moveSpeed.x, chaseSpeed * Time.deltaTime);

        //moveVector.z = -16;

        transform.position = moveSpeed;
        if (Input.GetButton("Jump"))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0, 90,0)), Time.deltaTime);
        }
    }
}
