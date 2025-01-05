using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MoveAction : BaseAction {
    [SerializeField] private Animator unitAnimator;
    private Vector3 targetPosition;
    private float moveSpeed = 4f;

    [FormerlySerializedAs("maxMovementRange")] [SerializeField]
    private int maxMoveDistance = 4;

    protected override void Awake() {
        base.Awake();
        unit = GetComponent<Unit>();
    }

    public override string GetActionName() {
        return "Move";
    }

    void Update() {
        if (!isActive) return;
        float stoppingDistance = .1f;
        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance) {
            transform.position += moveDirection * (Time.deltaTime * moveSpeed);
            unitAnimator.SetBool("IsWalking", true);
        } else {
            unitAnimator.SetBool("IsWalking", false);
            isActive = false;
            OnActionComplete?.Invoke();
        }

        float rotateSpeed = 10f;
        transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
    }

    public override void TakeAction(GridPosition targetPosition, Action callback) {
        OnActionComplete = callback;
        this.targetPosition = LevelGrid.Instance.GetWorldPosition(targetPosition);
        isActive = true;
    }

    public override bool IsValidActionGridPosition(GridPosition gridPosition) {
        List<GridPosition> validGridPositions = GetValidGridPositionList();
        return validGridPositions.Contains(gridPosition);
    }

    public override List<GridPosition> GetValidGridPositionList() {
        List<GridPosition> gridPositions = new List<GridPosition>();

        GridPosition currentGridPosition = unit.GetCurrentGridPosition();

        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++) {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++) {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition
                    testPosition = offsetGridPosition + currentGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testPosition)) {
                    continue;
                }

                if (testPosition == currentGridPosition) {
                    continue;
                }

                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testPosition)) {
                    continue;
                }

                gridPositions.Add(testPosition);
            }
        }

        return gridPositions;
    }
}