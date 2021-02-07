using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBlock : MonoBehaviour
{
    private Color startingColor;
    private TowerFactory towerFactory;
    private int gridSize;
    private bool isPlaceable;

    void Start() {
        GetGridSize();
        startingColor = GetTopColor();
        towerFactory = FindObjectOfType<TowerFactory>();
        isPlaceable = true;
    }

    void OnMouseOver() {
        SetTopColor(towerFactory.GetTowerPreviewColor());     
    }

    void OnMouseUpAsButton() {
        Vector2Int spawnPosition = GetGridPos(); 
        towerFactory.SpawnTower(this);
    }

    void OnMouseExit() {
        SetTopColor(startingColor);        
    }

    public Vector3 GetWorldPos() {
        Vector3 worldPosition = new Vector3( 
            GetGridPos().x * GetGridSize(),
            0,
            GetGridPos().y * GetGridSize()
        );

        return worldPosition;
    }

    public void SetPlaceability(bool placeability) {
        isPlaceable = placeability;
    }

    public bool GetPlaceability() {
        return (isPlaceable);
    } 

    private Vector2Int GetGridPos() {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.z / gridSize)
        );
    }

    private int GetGridSize() {
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
