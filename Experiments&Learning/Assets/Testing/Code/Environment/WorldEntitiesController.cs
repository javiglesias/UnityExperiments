using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldEntitiesController : MonoBehaviour
{
    public List<GameObject> Entitites;
    public int maxEntitiesCount = 100;
    // Update is called once per frame
    void Update()
    {
        if(Entitites != null)
        {
            foreach(var entity in Entitites)
            {
                List<GameObject> currentEntities = new List<GameObject>(GameObject.FindGameObjectsWithTag(entity.tag));
                try
                {
                    foreach(var controlEntity in currentEntities)
                    {
                        if(currentEntities.Count > maxEntitiesCount)
                        {
                            Destroy(controlEntity);
                            currentEntities.Remove(controlEntity);
                        }
                    }
                } catch
                {

                }
            }
        }
    }
}
