using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Unit : MonoBehaviour {
    private static int DEFAULT_ACTION_POINTS = 2;

    private GridPosition currentGridPosition;
    private MoveAction moveAction;
    private SpinAction spinAction;

    private BaseAction[] baseActionsArray;

    private int _actionPoints = DEFAULT_ACTION_POINTS;

    public static event EventHandler OnAnyActionPointsChanged;

    private void Awake() {
        moveAction = GetComponent<MoveAction>();
        spinAction = GetComponent<SpinAction>();
        baseActionsArray = GetComponentsInChildren<BaseAction>();
    }

    private void Start() {
        currentGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(currentGridPosition, this);
        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
    }

    void Update() {
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newGridPosition != currentGridPosition) {
            LevelGrid.Instance.UnitMovedGridPosition(this, currentGridPosition, newGridPosition);
            currentGridPosition = newGridPosition;
        }
    }

    public MoveAction GetMoveAction() {
        return moveAction;
    }

    public SpinAction GetSpinAction() {
        return spinAction;
    }

    public GridPosition GetCurrentGridPosition() {
        return currentGridPosition;
    }

    public BaseAction[] GetActionsArray() {
        return baseActionsArray;
    }

    public bool TrySpendActionPointsToTakeAction(BaseAction baseAction) {
        if (!CanSpendActionPointsToTakeAction(baseAction)) return false;
        SpendActionPoints(baseAction.GetActionCost());
        return true;
    }

    public int GetActionPoints() {
        return _actionPoints;
    }

    private bool CanSpendActionPointsToTakeAction(BaseAction baseAction) {
        bool canSpend = _actionPoints >= baseAction.GetActionCost();
        return canSpend;
    }

    private void SpendActionPoints(int actionCost) {
        _actionPoints -= actionCost;
    }

    private void TurnSystem_OnTurnChanged(object sender, EventArgs e) {
        _actionPoints = DEFAULT_ACTION_POINTS;
        OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
    }
}