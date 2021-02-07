using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] Tower spawnTower;
    [SerializeField] Transform spawnParent;
    [SerializeField] int maxTowers;
    private Dictionary<Vector3,Tower> towers = new Dictionary<Vector3,Tower>();

    public Color GetTowerPreviewColor() {
        if (towers.Count >= maxTowers) {
            return Color.red;
        } else {
            return spawnTower.GetColor();
        }
    }

    public void SpawnTower(TowerBlock spawnBlock) {
        if (!spawnBlock.GetPlaceability()) {
            RemoveTower(spawnBlock);
        } else {
            if (towers.Count >= maxTowers) { return; }
            AddTower(spawnBlock);
        }  
    }

    private void AddTower(TowerBlock spawnBlock) {
        Vector3 blockPosition = spawnBlock.GetWorldPos();
        Tower tower = GameObject.Instantiate(spawnTower,blockPosition,Quaternion.identity,spawnParent);

        towers.Add(blockPosition,tower);
        spawnBlock.SetPlaceability(false);
    }

    private void RemoveTower(TowerBlock spawnBlock) {
        Vector3 blockPosition = spawnBlock.GetWorldPos();        
        Tower tower = towers[blockPosition];

        Destroy(tower.transform.gameObject);
        spawnBlock.SetPlaceability(true);
        towers.Remove(blockPosition);
    }
}
