using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class NPCMovementController : MonoBehaviour
{
    public List<GameObject> NPCBounds = null;
    public static float NPCForceMovement = 10.0f;
    private Vector3[] movementDirection = new Vector3[6];
    private System.Random rng = new System.Random();
    private int direction = 0;
    private int oldDirection = 0;
    void Start()
    {
        movementDirection[0] = new Vector3(NPCForceMovement,0.0f,0.0f);
        movementDirection[1] = new Vector3(0.0f, NPCForceMovement,0.0f);
        movementDirection[2] = new Vector3(0.0f,0.0f, NPCForceMovement);
        movementDirection[3] = -movementDirection[0];
        movementDirection[4] = -movementDirection[1];
        movementDirection[5] = -movementDirection[2];
    }
    void OnCollisionEnter(Collision _bound)
    {
        if(_bound.gameObject.tag != "Ground")
            ChangeDirection();
    }
    void ChangeDirection(){
        oldDirection = direction;
        direction = rng.Next(0,5);
    }
    void OnCollisionStay(Collision _ground)
    {
        if(_ground.gameObject.tag == "Ground" && movementDirection != null)
        {
            if(oldDirection != direction)
                this.gameObject.GetComponent<Rigidbody>().AddForce(movementDirection[direction]);
            else if(oldDirection == direction)
                ChangeDirection();
        }
    }
}
