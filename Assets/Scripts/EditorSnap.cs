using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EditorSnap : MonoBehaviour
{
    private const int gridSize = 2;
    private Vector3 snapPosition;

    // Update is called once per frame
    void Update()
    {
        SnapToGrid();
        UpdateNameAndLabel();
    }

    void SnapToGrid() {
        snapPosition.x = Mathf.Round(transform.position.x / gridSize) * gridSize;
        snapPosition.y = 0;
        snapPosition.z = Mathf.Round(transform.position.z / gridSize) * gridSize;
        
        transform.position = snapPosition;
    }

    void UpdateNameAndLabel() {
        string coordText = (snapPosition.x / gridSize) + "," + (snapPosition.z / gridSize);
        TextMesh textMesh = GetComponentInChildren<TextMesh>();
        textMesh.text = coordText;
        gameObject.name = "Block (" + coordText + ")";
    }
}
