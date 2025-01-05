using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class UnitActionSystem : MonoBehaviour {
    public static UnitActionSystem Instance { get; private set; }

    public event EventHandler OnSelectedUnitChanged;
    public event EventHandler OnSelectedActionChanged;
    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayerMask;

    private BaseAction selectedAction;

    private bool isBusy;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There are two action systems on the scene");
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start() {
        SetSelectedUnit(selectedUnit);
    }

    private void Update() {
        if (isBusy) return;

        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (TryHandleUnitSelection()) return;

        HandleSelectedAction();
    }

    private void HandleSelectedAction() {
        if (!Input.GetMouseButtonDown(1)) return;

        GridPosition gridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());

        if (!selectedAction.IsValidActionGridPosition(gridPosition)) return;
        SetBusy();
        selectedAction.TakeAction(gridPosition, ClearBusy);
    }

    private void SetBusy() {
        isBusy = true;
    }

    private void ClearBusy() {
        isBusy = false;
    }

    private bool TryHandleUnitSelection() {
        if (!Input.GetMouseButtonDown(0)) return false;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit unitHit, float.MaxValue, unitLayerMask)) {
            if (unitHit.transform.TryGetComponent<Unit>(out Unit unit)) {
                if (unit == selectedUnit) {
                    return false;
                }

                SetSelectedUnit(unit);
                return true;
            }
        }

        return false;
    }

    private void SetSelectedUnit(Unit unit) {
        selectedUnit = unit;
        SetSelectedAction(unit.GetMoveAction());
        OnSelectedUnitChanged?.Invoke(this, new EventArgs());
    }

    public void SetSelectedAction(BaseAction action) {
        selectedAction = action;
        OnSelectedActionChanged?.Invoke(this, new EventArgs());
    }

    public Unit GetSelectedUnit() {
        return selectedUnit;
    }

    public BaseAction GetSelectedAction() {
        return selectedAction;
    }
}