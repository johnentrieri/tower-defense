using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyBlock))]
[ExecuteInEditMode]
public class EditorSnap : MonoBehaviour
{
    private EnemyBlock block;

    void Awake() {
        block = GetComponent<EnemyBlock>();
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
            block.GetGridPos().x * gridSize,
            0,
            block.GetGridPos().y * gridSize
        );
    }

    void UpdateNameAndLabel() {
        int gridSize = block.GetGridSize();
        string coordText = (block.GetGridPos().x) + "," + (block.GetGridPos().y);
        TextMesh textMesh = GetComponentInChildren<TextMesh>();
        textMesh.text = coordText;
        gameObject.name = "Enemy Block (" + coordText + ")";
    }
}
