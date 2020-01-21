using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController playerController;

    private float horizontalMove;
    private float verticalMove;

    private Vector3 inputPlayer;
    private Vector3 movePlayer;

    public Transform lookAt;

    // Movement
    [SerializeField] private float moveSpeed = 15.0f;
    [SerializeField] private float rotationSpeed = 3.0f;

    // Jump
    private float forceVertical = 0.0f;
    [SerializeField] private float forceJump = 5.0f;
    [SerializeField] private float gravity = Physics.gravity.y;

    // Sound
    public AudioClip jumpSound;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        if (playerController.isGrounded)
        {
            inputPlayer = new Vector3(0.0f, 0.0f, verticalMove);
            // Velocidad diagonal no sea mayor a 1 (max velocidad). See: Debug.Log(playerController.velocity.magnitude)
            inputPlayer = Vector3.ClampMagnitude(inputPlayer, 1);
            inputPlayer = transform.TransformDirection(inputPlayer);
            inputPlayer *= moveSpeed;

            if (Input.GetButton("Jump"))
            {
                Game._soundManager.playOneShot(jumpSound, 1f);
                forceVertical = forceJump;
            }
        }
        else
        {
            forceVertical += gravity * Time.deltaTime;
        }

        inputPlayer.y = forceVertical;

        playerController.Move(inputPlayer * Time.deltaTime);

        transform.Rotate(0.0f, horizontalMove * rotationSpeed * Time.deltaTime, 0.0f);

        /*
        inputPlayer = new Vector3(horizontalMove, 0.0f, verticalMove);
        // Velocidad diagonal no sea mayor a 1 (max velocidad). See: Debug.Log(playerController.velocity.magnitude)
        inputPlayer = Vector3.ClampMagnitude(inputPlayer, 1);

        //Quaternion rotation = Quaternion.LookRotation(direction);
        //lookAt.parent.rotation = rotation;
        
        if (inputPlayer != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(inputPlayer);
            lookAt.parent.rotation = Quaternion.Lerp(lookAt.parent.rotation, rotation, rotationSpeed * Time.deltaTime);
        }

        //lookAt.parent.rotation = Quaternion.Lerp(lookAt.parent.rotation, rotation, 60 * Time.deltaTime);

        playerController.Move(inputPlayer * moveSpeed * Time.deltaTime);
        */
        /*
        if (playerController.isGrounded)
        {
            Debug.Log("CONTROLLER IS GROUNDED");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("hola");
                Game._soundManager.playOneShot(jumpSound, 1f);

                gravityForce = jumpForce;
            }
        }
        else
        {
            gravityForce += gravity * Time.deltaTime;
        }
        
        moveVector.y = gravityForce;
        */
    }
}
