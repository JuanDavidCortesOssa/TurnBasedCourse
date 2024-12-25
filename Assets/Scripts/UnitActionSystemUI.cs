using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystemUI : MonoBehaviour {
    [SerializeField] private Transform actionButtonsParent;
    [SerializeField] private GameObject actionButtonPrefab;

    private void Start() {
        CreateUnitActionButtons();
        UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
    }

    private void CreateUnitActionButtons() {
        foreach (Transform actionButton in actionButtonsParent) {
            Destroy(actionButton.gameObject);
        }

        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
        BaseAction[] baseActions = selectedUnit.GetActionsArray();

        foreach (BaseAction baseAction in baseActions) {
            GameObject buttonTransform = Instantiate(actionButtonPrefab, actionButtonsParent);
            ActionButtonUI actionButtonUI = buttonTransform.GetComponent<ActionButtonUI>();

            actionButtonUI.SetBaseAction(baseAction);
        }
    }

    private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs empty) {
        CreateUnitActionButtons();
    }
}