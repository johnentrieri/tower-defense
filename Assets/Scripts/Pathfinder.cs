using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour 
{
    [SerializeField] Block startBlock, endBlock;
    private Dictionary<Vector2Int,Block> grid = new Dictionary<Vector2Int, Block>();
    private Dictionary<Block,Block> queueMap = new Dictionary<Block, Block>();
    private List<Block> shortestPath = new List<Block>();
    Queue<Block> queue = new Queue<Block>();
    private Vector2Int[] allowedDirections = {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };


    public List<Block> GetShortestPath() {
        LoadBlocks();
        SetStartAndEndColors();
        Pathfind();

        return shortestPath;
    }

    public Block GetStartBlock() {
        return startBlock;
    }

    private void Pathfind() {
        queue.Clear();
        queueMap.Clear();
        shortestPath.Clear();

        EnqueueAndMap(startBlock,startBlock);

        while (queue.Count > 0) {
            Block searchBlock = queue.Dequeue();
            if (CheckEndBlock(searchBlock)) { return; }          
            EnqueueNeighbors(searchBlock);
        }
    }

    private void EnqueueNeighbors(Block block) {
        foreach (Vector2Int direction in allowedDirections) {

            Vector2Int explorePos = block.GetGridPos() + direction;
            if (!grid.ContainsKey(explorePos)) { continue; }

            Block neighbor = grid[explorePos];
            if (queueMap.ContainsKey(neighbor)) { continue; }

            EnqueueAndMap(neighbor, block);
        }
    }

    private void TracePath(Block end) {
        shortestPath.Add(end);
        while ( !shortestPath.Contains(startBlock)) {
            Block lastItem = shortestPath[shortestPath.Count-1];
            Block queuer = queueMap[lastItem];

            if (queuer != startBlock) { queuer.SetTopColor(Color.yellow); }
            shortestPath.Add(queuer);
        }
        shortestPath.Reverse();
        shortestPath.Remove(startBlock);
    }

    private void EnqueueAndMap(Block block, Block queuedBy) {
        queue.Enqueue(block);
        queueMap.Add(block,queuedBy);
    }

    private bool CheckEndBlock(Block block) {
        if (block.GetGridPos() == endBlock.GetGridPos()) {
            TracePath(block);
            return true;
        } else {
            return false;
        }
    }

    private void SetStartAndEndColors() {
        startBlock.SetTopColor(Color.green);
        endBlock.SetTopColor(Color.red);
    }

    private void LoadBlocks() {
        grid.Clear();
        GameObject blockParent = startBlock.transform.parent.gameObject;
        Block[] blocks = blockParent.GetComponentsInChildren<Block>();
        foreach(Block block in blocks) {
            if (grid.ContainsKey(block.GetGridPos())) {
                Debug.LogWarning("Overlapping Block: " + block);
            } else {
                grid.Add( block.GetGridPos(), block);
            }
        }
    }
}
