using UnityEngine;
using System.Collections;

public class Movimiento : MonoBehaviour
{
    private float playerSpeed;
    public float walkSpeed = 2f;
    public float jumpHeight = 3f;
    private float yRot;
    private Rigidbody rigidBody;
    public GameObject right_hand;

    void Start()
    {
        playerSpeed = walkSpeed;
        rigidBody = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        yRot += Input.GetAxis("Mouse X");
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, yRot, transform.localEulerAngles.z);

        if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
            rigidBody.velocity += transform.right * Input.GetAxisRaw("Horizontal") * playerSpeed;
        if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < 0.5f)
            rigidBody.velocity += transform.forward * Input.GetAxisRaw("Vertical") * playerSpeed;
    }
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Suelo_saltable" && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }
    void Jump()
    {
        rigidBody.velocity += Vector3.up * jumpHeight;
    }
    void OnMouseDown()
    {
        if (Input.GetMouseButton(0))
            Attack_right();
        else
            Attack_left();
    }
    void Attack_right()
    {
        
    }
    void Attack_left()
    {

    }
}
