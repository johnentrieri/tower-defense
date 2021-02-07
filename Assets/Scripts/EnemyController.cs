using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{   
    [Header("Enemy Characteristics")]
    [Tooltip("Enemy Game Object")][SerializeField] GameObject enemy;
    [Tooltip("Enemy Health")][SerializeField] int enemyHealth;
    [Tooltip("Spawn Parent")][SerializeField] Transform spawnParent;
    [Tooltip("Enemy Count")][SerializeField] int enemyCount = 1;
    [Tooltip("Enemies per Second")][SerializeField] float spawnRate = 1.0f;
    [Tooltip("Delay Before First Spawn")][SerializeField] float spawnDelay = 0.0f;
    


    [Header("Movement Parameters")]
    [Tooltip("Blocks per Second")][SerializeField] float speed = 1.0f;
    [Tooltip("Frames per Block")][SerializeField] int steps = 60;


    private Pathfinder pathfinder;
    private List<EnemyBlock> path;


    void Start() {
        pathfinder = GetComponent<Pathfinder>();        
        path = pathfinder.GetShortestPath();

        StartCoroutine(SpawnEnemies());
    }
    private IEnumerator SpawnEnemies() {

        yield return new WaitForSeconds(spawnDelay);

        for(int i=0 ; i < enemyCount ; i++) {

            EnemyBlock spawnBlock = pathfinder.GetStartBlock();
            int gridSize = spawnBlock.GetGridSize();
            int startX = spawnBlock.GetGridPos().x * gridSize;
            int startZ = spawnBlock.GetGridPos().y * gridSize;
            Vector3 startPos = new Vector3(startX,0,startZ);

            GameObject spawnedEnemy = GameObject.Instantiate(enemy, startPos, Quaternion.identity, spawnParent);
            spawnedEnemy.SetActive(true);

            spawnedEnemy.GetComponentInChildren<Enemy>().SetSpawnParent(spawnParent);
            spawnedEnemy.GetComponentInChildren<Enemy>().SetEnemyHealth(enemyHealth);

            StartCoroutine(MoveEnemyAlongPath(spawnedEnemy));
            yield return new WaitForSeconds(1.0f / spawnRate);
        }
    }

    private IEnumerator MoveEnemyAlongPath(GameObject movingEnemy) {       
        foreach(EnemyBlock block in path) {
            if (movingEnemy == null) { break; }  
            Vector3 endPos = block.transform.position;
            float d = Vector3.Distance(movingEnemy.transform.position,endPos);
            float maxDistancePerStep = d / steps;
            for (float i = 0; i < steps; i++) {   
                if (movingEnemy == null) { break; }             
                Vector3 nextPos = Vector3.MoveTowards(movingEnemy.transform.position,endPos,maxDistancePerStep);            
                movingEnemy.transform.position = nextPos;
                if ( pathfinder.GetEndBlock().GetWorldPos() == movingEnemy.transform.position) {
                    movingEnemy.GetComponentInChildren<Enemy>().ProcessGoalReached();
                }
                yield return new WaitForSeconds(1.0f / (speed * steps));                
            }
        }
    }
}
