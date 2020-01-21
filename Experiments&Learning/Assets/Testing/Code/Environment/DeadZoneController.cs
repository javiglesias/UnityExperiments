using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneController : MonoBehaviour
{
    void OnCollisionEnter(Collision deadCollision)
    {
        Destroy(deadCollision.gameObject);
    }
}
