using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Block startBlock;
    [SerializeField] Block endBlock;
    [SerializeField] EnemyController enemy;
    private Dictionary<Vector2Int,Block> grid = new Dictionary<Vector2Int, Block>();
    private Dictionary<Block,Block> queueMap = new Dictionary<Block, Block>();
    private List<Block> shortestPath = new List<Block>();
    private int checkedBlocks;

    Queue<Block> queue = new Queue<Block>();

    private Vector2Int[] allowedDirections = {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    // Start is called before the first frame update
    void Start()
    {
        LoadBlocks();
        SetStartAndEndColors();
        Pathfind();

        enemy.Spawn(startBlock);
        enemy.MoveAlongPath(shortestPath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Pathfind() {
        queue.Clear();
        queueMap.Clear();
        shortestPath.Clear();
        checkedBlocks = 0;

        EnqueueAndMap(startBlock,startBlock);

        while (queue.Count > 0) {
            Block searchBlock = queue.Dequeue();
            print("Searching from " + searchBlock);
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
        checkedBlocks++;
        if (block.GetGridPos() == endBlock.GetGridPos()) {
            print("End Block Found At " + block.GetGridPos());
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
        Block[] blocks = GetComponentsInChildren<Block>();
        foreach(Block block in blocks) {
            if (grid.ContainsKey(block.GetGridPos())) {
                Debug.LogWarning("Overlapping Block: " + block);
            } else {
                grid.Add( block.GetGridPos(), block);
            }
        }
    }
}
