using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveCilinderController : MonoBehaviour
{
    public GameObject explosionType = null;
    public int MaxExplosionsGenerated = 10;
    private List<GameObject> explosionsGenerated = null;

    void Start()
    {
        explosionsGenerated = new List<GameObject>();
    }
    void OnCollisionEnter(Collision _collision)
    {
        if(_collision.gameObject.tag != "Ground" && _collision.gameObject.tag != "ExplosionParticles")
        {
            if(explosionsGenerated.Count < MaxExplosionsGenerated)
            {
                GameObject newExplosion =Instantiate(explosionType, _collision.contacts[0].point, Quaternion.identity);
                newExplosion.transform.parent = this.transform;
                explosionsGenerated.Add(newExplosion);


            }
            else if(explosionsGenerated.Count >= MaxExplosionsGenerated)
            {
                GameObject FIFOItem = explosionsGenerated[0];
                explosionsGenerated.RemoveAt(0);
                Destroy(FIFOItem);
            }
        }
    }
}
