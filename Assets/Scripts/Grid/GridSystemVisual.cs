using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemVisual : MonoBehaviour {
    public static GridSystemVisual Instance { get; private set; }

    [SerializeField] private Transform gridSystemVisual;

    private GridSystemVisualSingle[,] gridSystemVisuals;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There are two action systems on the scene");
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start() {
        int width = LevelGrid.Instance.GetWidth();
        int height = LevelGrid.Instance.GetHeight();

        gridSystemVisuals = new GridSystemVisualSingle[width, height];

        for (int x = 0; x < width; x++) {
            for (int z = 0; z < height; z++) {
                GridPosition gridPosition = new GridPosition(x, z);
                Transform gridVisualTransform = Instantiate(gridSystemVisual,
                    LevelGrid.Instance.GetWorldPosition(gridPosition),
                    Quaternion.identity);
                GridSystemVisualSingle gridSystemVisualSingle =
                    gridVisualTransform.GetComponent<GridSystemVisualSingle>();
                gridSystemVisuals[x, z] = gridSystemVisualSingle;
            }
        }
    }

    private void Update() {
        UpdateGridVisual();
    }

    public void HideAllGridPositions() {
        foreach (GridSystemVisualSingle gridVisual in gridSystemVisuals) {
            gridVisual.Hide();
        }
    }

    public void ShowGridPositionList(List<GridPosition> gridPositions) {
        foreach (GridPosition gridPosition in gridPositions) {
            gridSystemVisuals[gridPosition.x, gridPosition.z].Show();
        }
    }

    private void UpdateGridVisual() {
        HideAllGridPositions();
        List<GridPosition> validGridPositions =
            UnitActionSystem.Instance.GetSelectedAction().GetValidGridPositionList();
        ShowGridPositionList(validGridPositions);
    }
}