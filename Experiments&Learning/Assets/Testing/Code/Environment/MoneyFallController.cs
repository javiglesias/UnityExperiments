using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyFallController : MonoBehaviour
{
    public GameObject Billet = null;
    private List<GameObject> Billets = null;
    public int maxBillets = 100;
    void Update()
    {
        Billets = new List<GameObject>(GameObject.FindGameObjectsWithTag("Billet"));
        if(Billets.Count < maxBillets)
        {
            GameObject newBillet = Instantiate(Billet, gameObject.transform.position, Quaternion.identity);
            newBillet.transform.parent = this.transform;
        }
        else if(Billets.Count >= maxBillets)
        {
            GameObject FIFOItem = Billets[0];
            Billets.RemoveAt(0);
            Destroy(FIFOItem);
        }
    }
}
