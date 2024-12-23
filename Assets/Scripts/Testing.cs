using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour {
    [SerializeField] private Unit unit;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            List<GridPosition> validGridPositions = unit.GetMoveAction().GetValidGridPositionList();
            GridSystemVisual.Instance.HideAllGridPositions();
            GridSystemVisual.Instance.ShowGridPositionList(validGridPositions);
        }
    }
}