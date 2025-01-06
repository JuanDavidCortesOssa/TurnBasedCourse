using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour {
    protected Unit unit;
    protected bool isActive;

    public Action OnActionComplete;

    protected virtual void Awake() {
        unit = GetComponent<Unit>();
    }

    public abstract string GetActionName();

    public abstract void TakeAction(GridPosition gridPosition, Action onComplete);

    public virtual bool IsValidActionGridPosition(GridPosition gridPosition) {
        List<GridPosition> validGridPositions = GetValidGridPositionList();
        return validGridPositions.Contains(gridPosition);
    }

    public abstract List<GridPosition> GetValidGridPositionList();

    public virtual int GetActionCost() {
        return 1;
    }
}