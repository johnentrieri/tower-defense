using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlock : MonoBehaviour

{
    private const int gridSize = 2;

    public int GetGridSize() {
        return gridSize;
    }

    public Vector2Int GetGridPos() {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.z / gridSize)
        );
    }

    public Vector3 GetWorldPos() {
        Vector3 worldPosition = new Vector3( 
            GetGridPos().x * GetGridSize(),
            0,
            GetGridPos().y * GetGridSize()
        );

        return worldPosition;
    }

    public void SetTopColor(Color color) {
        MeshRenderer topMeshRenderer = transform.Find("Top").GetComponent<MeshRenderer>();
        topMeshRenderer.material.color = color;
    }

    public Color GetTopColor() {
        MeshRenderer topMeshRenderer = transform.Find("Top").GetComponent<MeshRenderer>();
        return topMeshRenderer.material.color;
    }
}
