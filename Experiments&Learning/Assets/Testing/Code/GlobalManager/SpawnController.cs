using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject[] tileComponents;

    private int sizeComponents;

    //public Vector3 center;
    //public Vector3 size;

    public float percentage = 0;

    // Use this for initialization
    void Start()
    {
        sizeComponents = tileComponents.Length;

        SpawnComponent();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SpawnComponent()
    {
        float numberRandom = Random.value;

        if (Random.value < percentage / 100)
        {
            int randomComponent = Random.Range(0, sizeComponents);

            GameObject component = Instantiate(tileComponents[randomComponent]);

            //component.setParent(transform);
            //component.transform.SetParent(transform);

            // Create in cube Z always 0 => Vector3(Random, Random, 0);
            component.transform.position = transform.position + new Vector3(Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2), 0, 0);

            //Instantiate(component, pos, Quaternion.identity);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0.5f);
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
}
