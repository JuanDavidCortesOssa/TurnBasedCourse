using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = Unity.Mathematics.Random;

public class SpinAction : BaseAction {
    private float totalSpinAmount = 0f;
    private Action spinCompleteDelegate;

    public override string GetActionName() {
        return "Spin";
    }

    public void Update() {
        if (!isActive) return;

        float spinAddAmount = 360f * Time.deltaTime;
        totalSpinAmount += spinAddAmount;
        transform.eulerAngles += new Vector3(0, spinAddAmount, 0);

        if (!(totalSpinAmount >= 360f)) return;
        totalSpinAmount = 0f;
        isActive = false;

        spinCompleteDelegate?.Invoke();
    }

    public override void TakeAction(GridPosition gridPosition, Action callback) {
        spinCompleteDelegate = callback;
        totalSpinAmount = 0;
        isActive = true;
    }

    public override List<GridPosition> GetValidGridPositionList() {
        GridPosition currentGridPosition = unit.GetCurrentGridPosition();
        return new List<GridPosition>() { currentGridPosition };
    }
}