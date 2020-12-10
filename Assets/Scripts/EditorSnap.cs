using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Block))]
[ExecuteInEditMode]
public class EditorSnap : MonoBehaviour
{
    private Block block;

    void Awake() {
        block = GetComponent<Block>();
    }

    // Update is called once per frame
    void Update()
    {
        SnapToGrid();
        UpdateNameAndLabel();
    }

    void SnapToGrid() {
        int gridSize = block.GetGridSize();
        transform.position = new Vector3(
            block.GetGridPos().x,
            0,
            block.GetGridPos().y
        );
    }

    void UpdateNameAndLabel() {
        int gridSize = block.GetGridSize();
        string coordText = (block.GetGridPos().x / gridSize) + "," + (block.GetGridPos().y / gridSize);
        TextMesh textMesh = GetComponentInChildren<TextMesh>();
        textMesh.text = coordText;
        gameObject.name = "Block (" + coordText + ")";
    }
}
