using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitActionSystem : MonoBehaviour {
    public static UnitActionSystem Instance { get; private set; }

    public event EventHandler OnSelectedUnitChanged;
    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayerMask;

    private bool isBusy;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There are two action systems on the scene");
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Update() {
        if (isBusy) return;

        if (Input.GetMouseButtonDown(0)) {
            if (TryHandleUnitSelection()) return;

            GridPosition gridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());

            if (selectedUnit.GetMoveAction().IsValidActionGridPosition(gridPosition)) {
                selectedUnit.GetMoveAction().Move(gridPosition, ClearBusy);
                SetBusy();
            }
        }

        if (Input.GetMouseButtonDown(1)) {
            selectedUnit.GetSpinAction().Spin(ClearBusy);
            SetBusy();
        }
    }

    private void SetBusy() {
        isBusy = true;
    }

    private void ClearBusy() {
        isBusy = false;
    }

    private bool TryHandleUnitSelection() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit unitHit, float.MaxValue, unitLayerMask)) {
            if (unitHit.transform.TryGetComponent<Unit>(out Unit unit)) {
                SetSelectedUnit(unit);
                return true;
            }
        }

        return false;
    }

    private void SetSelectedUnit(Unit unit) {
        selectedUnit = unit;
        OnSelectedUnitChanged?.Invoke(this, new EventArgs());
    }

    public Unit GetSelectedUnit() {
        return selectedUnit;
    }
}