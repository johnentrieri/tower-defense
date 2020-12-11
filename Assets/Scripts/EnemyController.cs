using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{   
    [Tooltip("Blocks Per Second")][SerializeField] float speed = 1.0f;
    [Tooltip("Frames Per Block")][SerializeField] int steps = 60;
    private List<Block> path;

    public void Spawn(Block block) {
        gameObject.SetActive(true);
        transform.position = new Vector3(
            block.GetGridPos().x * block.GetGridSize(),
            0,
            block.GetGridPos().y * block.GetGridSize()
        );        
    }

    public void MoveAlongPath(List<Block> blockPath) {
        path = blockPath;
        StartCoroutine(Move());
    }

    private IEnumerator Move() {       
        foreach(Block block in path) {
            Vector3 endPos = block.transform.position;
            float d = Vector3.Distance(transform.position,endPos);
            float maxDistancePerStep = d / steps;
            for (float i = 0; i < steps; i++) {               
                Vector3 nextPos = Vector3.MoveTowards(transform.position,endPos,maxDistancePerStep);            
                transform.position = nextPos;
                yield return new WaitForSeconds(1.0f / (speed * steps));
            }
        }
    }
}
