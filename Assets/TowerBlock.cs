using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBlock : MonoBehaviour
{
    [SerializeField] Color previewColor = Color.magenta;
    [SerializeField] Tower spawnTower;
    [SerializeField] Transform spawnParent;
    private Color startingColor;
    private int gridSize;
    private Tower tower;

    void Start() {
        GetGridSize();
        startingColor = GetTopColor();      
    }

    void OnMouseOver() {
        SetTopColor(previewColor);        
    }

    void OnMouseUpAsButton() {
        if (tower != null) {
            print("!");
            Destroy(tower.gameObject);
        } else {
            Vector2Int spawnPosition = GetGridPos();
            tower = SpawnTower(spawnPosition);   
        }     
    }

    void OnMouseExit() {
        SetTopColor(startingColor);        
    }

    
    public void SetPreviewColor(Color color) {
        previewColor = color;
    }

    public void SetSpawnTower(Tower tower) {
        print("Spawn");
    }

    private Tower SpawnTower(Vector2Int spawnPos) {
        Vector3 spawnPosition = new Vector3( 
            spawnPos.x * gridSize,
            0,
            spawnPos.y * gridSize
        );
        Tower spawnedTower = GameObject.Instantiate(spawnTower,spawnPosition,Quaternion.identity,spawnParent);
        return spawnedTower;

    }

    private void GetGridSize() {
        EnemyBlock tempBlock = gameObject.AddComponent<EnemyBlock>();
        gridSize = tempBlock.GetGridSize();
        Destroy(tempBlock);
    }

    private Vector2Int GetGridPos() {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.z / gridSize)
        );
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
