using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EditorSnap : MonoBehaviour
{
    [SerializeField] float gridSize = 2.0f;
    private Vector3 snapPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        snapPosition.x = Mathf.Round(transform.position.x / gridSize) * gridSize;
        snapPosition.y = 0;
        snapPosition.z = Mathf.Round(transform.position.z / gridSize) * gridSize;
        
        transform.position = snapPosition;
    }
}
