using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour 
{
    [SerializeField] EnemyBlock startBlock, endBlock;
    private Dictionary<Vector2Int,EnemyBlock> grid = new Dictionary<Vector2Int, EnemyBlock>();
    private Dictionary<EnemyBlock,EnemyBlock> queueMap = new Dictionary<EnemyBlock, EnemyBlock>();
    private List<EnemyBlock> shortestPath = new List<EnemyBlock>();
    Queue<EnemyBlock> queue = new Queue<EnemyBlock>();
    private Vector2Int[] allowedDirections = {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };


    public List<EnemyBlock> GetShortestPath() {
        LoadBlocks();
        Pathfind();

        return shortestPath;
    }

    public EnemyBlock GetStartBlock() {
        return startBlock;
    }

    private void Pathfind() {
        queue.Clear();
        queueMap.Clear();
        shortestPath.Clear();

        EnqueueAndMap(startBlock,startBlock);

        while (queue.Count > 0) {
            EnemyBlock searchBlock = queue.Dequeue();
            if (CheckEndBlock(searchBlock)) { return; }          
            EnqueueNeighbors(searchBlock);
        }
    }

    private void EnqueueNeighbors(EnemyBlock block) {
        foreach (Vector2Int direction in allowedDirections) {

            Vector2Int explorePos = block.GetGridPos() + direction;
            if (!grid.ContainsKey(explorePos)) { continue; }

            EnemyBlock neighbor = grid[explorePos];
            if (queueMap.ContainsKey(neighbor)) { continue; }

            EnqueueAndMap(neighbor, block);
        }
    }

    private void TracePath(EnemyBlock end) {
        shortestPath.Add(end);
        while ( !shortestPath.Contains(startBlock)) {
            EnemyBlock lastItem = shortestPath[shortestPath.Count-1];
            EnemyBlock queuer = queueMap[lastItem];
            shortestPath.Add(queuer);
        }
        shortestPath.Reverse();
        shortestPath.Remove(startBlock);
    }

    private void EnqueueAndMap(EnemyBlock block, EnemyBlock queuedBy) {
        queue.Enqueue(block);
        queueMap.Add(block,queuedBy);
    }

    private bool CheckEndBlock(EnemyBlock block) {
        if (block.GetGridPos() == endBlock.GetGridPos()) {
            TracePath(block);
            return true;
        } else {
            return false;
        }
    }

    private void LoadBlocks() {
        grid.Clear();
        GameObject blockParent = startBlock.transform.parent.gameObject;
        EnemyBlock[] blocks = blockParent.GetComponentsInChildren<EnemyBlock>();
        foreach(EnemyBlock block in blocks) {
            if (grid.ContainsKey(block.GetGridPos())) {
                Debug.LogWarning("Overlapping Block: " + block);
            } else {
                grid.Add( block.GetGridPos(), block);
            }
        }
    }
}
