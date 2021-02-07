using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] Tower spawnTower;
    [SerializeField] Transform spawnParent;
    [SerializeField] int maxTowers;
    private int towersPlaced;

    void Start() {
        towersPlaced = 0;
    }

    public Color GetTowerColor() {
        return spawnTower.GetColor();
    }

    public void SpawnTower(TowerBlock spawnBlock) {
        if (towersPlaced >= maxTowers) { return; }

        Vector3 spawnPosition = new Vector3( 
            spawnBlock.GetGridPos().x * spawnBlock.GetGridSize(),
            0,
            spawnBlock.GetGridPos().y * spawnBlock.GetGridSize()
        );

        GameObject.Instantiate(spawnTower,spawnPosition,Quaternion.identity,spawnParent);
        towersPlaced++;
    }

    //TODO - Destroy Tower
}
