using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject ground;
    public List<GameObject> enemyTypes = null;
    public int maxConcurrentEnemies = 10;
    public int spawnRadius = 10;
    private System.Random rnd;
    private List<GameObject> enemies;
    // Start is called before the first frame update
    void Start()
    {
        rnd = new System.Random();
        enemies = new List<GameObject>();
    }
    void Update(){
        //enemies = GameObject.FindGameObjectsWithTag("BasicEnemy");
        try
        {
            foreach(var enemy in enemies)
            {
                if(enemy.transform.position.y < 0)
                {
                    Destroy(enemy);
                    enemies.Remove(enemy);
                    continue;
                }
            }
        }
        catch(InvalidOperationException ex)
        {
            Debug.Log("mal rollo");
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.E) && enemies.Count < maxConcurrentEnemies)
        {
            if(enemyTypes == null) return;
            int enemyType = rnd.Next(0, enemyTypes.Count);
            float maxSpawnEnemyX = this.transform.position.x + spawnRadius;
            float minSpawnEnemyX = this.transform.position.x - spawnRadius;
            float maxSpawnEnemyZ = this.transform.position.z + spawnRadius;
            float minSpawnEnemyZ = this.transform.position.z - spawnRadius;
            float newSpawnX = rnd.Next((int)minSpawnEnemyX, (int)maxSpawnEnemyX);
            float newSpawnY = this.transform.position.y;
            float newSpawnZ = rnd.Next((int)minSpawnEnemyZ, (int)maxSpawnEnemyZ);
            enemies.Add(Instantiate(enemyTypes[enemyType], new Vector3(newSpawnX ,newSpawnY ,newSpawnZ),
            Quaternion.identity));
        }
    }
}
