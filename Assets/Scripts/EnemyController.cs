using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] List<Block> path;
    [Tooltip("Blocks Per Second")][SerializeField] float speed = 1.0f;
    [Tooltip("Frames Per Block")][SerializeField] int steps = 60;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(MoveAlongPath());      
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator MoveAlongPath() {       
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
