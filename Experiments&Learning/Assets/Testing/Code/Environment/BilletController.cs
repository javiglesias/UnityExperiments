using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BilletController : MonoBehaviour
{
    public GameObject Parent = null;
    // Start is called before the first frame update
    void Start()
    {
        if(Parent != null)
            gameObject.transform.parent = Parent.transform;
    }
}
