using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject {
    private GridSystem gridSystem;
    private GridPosition gridPosition;
    private List<Unit> units;

    public GridObject(GridSystem gridSystem, GridPosition gridPosition) {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
        units = new List<Unit>();
    }

    public override string ToString() {
        string gridString = gridPosition.ToString();

        foreach (Unit unit in units) {
            gridString += "\n" + unit.name;
        }

        return gridString;
    }

    public GridPosition GetGridPosition() {
        return this.gridPosition;
    }

    public bool HasAnyUnit() {
        return units.Count > 0;
    }

    public void RemoveUnit(Unit unit) {
        units.Remove(unit);
    }

    public void AddUnit(Unit unit) {
        units.Add(unit);
    }

    public List<Unit> GetUnitList() {
        return units;
    }
}