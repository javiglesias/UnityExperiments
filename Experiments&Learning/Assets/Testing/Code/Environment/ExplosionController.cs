using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public float forceExplosion = 1.0f;
    public AudioSource explosionSound = null;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            explosionSound = this.GetComponent<AudioSource>();
            explosionSound.Play(0);
            foreach(Transform child in transform)
            {
                switch(child.name)
                {
                    case "X-direction":
                        child.GetComponent<Rigidbody>().AddForce(Vector3.right* forceExplosion);
                        break;
                    case "!X-direction":
                        child.GetComponent<Rigidbody>().AddForce(Vector3.right * forceExplosion);
                        break;
                    case "Y-direction":
                        child.GetComponent<Rigidbody>().AddForce(Vector3.up * forceExplosion);
                        break;
                    case "!Y-direction":
                        child.GetComponent<Rigidbody>().AddForce(Vector3.down * forceExplosion);
                        break;
                    case "Z-direction":
                        child.GetComponent<Rigidbody>().AddForce(Vector3.forward * forceExplosion);
                        break;
                    case "!Z-direction":
                        child.GetComponent<Rigidbody>().AddForce(Vector3.back * forceExplosion);
                        break;
                    default:
                    break;
                }
            }
        } catch
        {

        }
    }
    void OnCollisionEnter(Collision _ground)
    {
        if(_ground.transform.tag == "Ground")
            Destroy(this);
    }
}
