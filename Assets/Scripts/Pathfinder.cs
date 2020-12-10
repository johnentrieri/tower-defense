using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Block startBlock;
    [SerializeField] Block endBlock;
    private Dictionary<Vector2Int,Block> grid = new Dictionary<Vector2Int, Block>();

    // Start is called before the first frame update
    void Start()
    {
        LoadBlocks();
        SetStartAndEndColors();        
    }

    // Update is called once per frame
    void Update()
    {
        
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
