using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem
{
    private int width;
    private int height;
    private float cellSize;

    public GridSystem(int width, int height, float cellSize) 
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        for(int x = 0; x < height; x++)
        {
            for(int z = 0; z < width; z++)
            {
                Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z) + Vector3.right * 0.2f, Color.red, 1000);
                Debug.Log("Created line " + x + " , " + z);
            }
        }
    }

    public Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, 0, z) * cellSize;
    }

    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return new GridPosition(Mathf.RoundToInt(worldPosition.x), Mathf.RoundToInt(worldPosition.z));
    }
}
