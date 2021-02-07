using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBlock : MonoBehaviour
{
    private Color startingColor;
    private TowerFactory towerFactory;
    private int gridSize;
    private bool isTowerPlaced;

    void Start() {
        GetGridSize();
        startingColor = GetTopColor();
        towerFactory = FindObjectOfType<TowerFactory>();
        isTowerPlaced = false;
    }

    void OnMouseOver() {
        SetTopColor(towerFactory.GetTowerColor());     
    }

    void OnMouseUpAsButton() {
        Vector2Int spawnPosition = GetGridPos();
        if (!isTowerPlaced) { 
            towerFactory.SpawnTower(this);
            isTowerPlaced = true;
        }
    }

    void OnMouseExit() {
        SetTopColor(startingColor);        
    }

    public Vector2Int GetGridPos() {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.z / gridSize)
        );
    }

    public int GetGridSize() {
        EnemyBlock tempBlock = gameObject.AddComponent<EnemyBlock>();
        gridSize = tempBlock.GetGridSize();
        Destroy(tempBlock);
        return gridSize;        
    }    

    private void SetTopColor(Color color) {
        MeshRenderer topMeshRenderer = transform.Find("Top").GetComponent<MeshRenderer>();
        topMeshRenderer.material.color = color;
    }

    private Color GetTopColor() {
        MeshRenderer topMeshRenderer = transform.Find("Top").GetComponent<MeshRenderer>();
        return topMeshRenderer.material.color;
    }

}
