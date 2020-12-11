using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Block startBlock;
    [SerializeField] Block endBlock;
    private Dictionary<Vector2Int,Block> grid = new Dictionary<Vector2Int, Block>();

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
        ExploreNeighbors(startBlock);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ExploreNeighbors(Block block) {
        foreach (Vector2Int direction in allowedDirections) {
            Vector2Int explorePos = block.GetGridPos() + direction;
            if (grid.ContainsKey(explorePos)) { grid[explorePos].SetTopColor(Color.yellow); }
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
