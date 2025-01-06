using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitActionSystemUI : MonoBehaviour {
    [SerializeField] private Transform actionButtonsParent;
    [SerializeField] private GameObject actionButtonPrefab;
    [SerializeField] private TextMeshProUGUI actionPointsText;

    private List<ActionButtonUI> actionButtonsUIS = new List<ActionButtonUI>();

    private void Awake() {
        actionButtonsUIS = new List<ActionButtonUI>();
    }

    private void Start() {
        UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
        UnitActionSystem.Instance.OnSelectedActionChanged += UnitActionSystem_OnSelectedActionChanged;
        UnitActionSystem.Instance.OnActionStarted += UnitActionSystem_OnActionStarted;

        CreateUnitActionButtons();
        UpdateSelectedVisual();
        UpdateActionPoints();
    }

    private void CreateUnitActionButtons() {
        foreach (Transform actionButton in actionButtonsParent) {
            Destroy(actionButton.gameObject);
        }

        actionButtonsUIS.Clear();

        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
        BaseAction[] baseActions = selectedUnit.GetActionsArray();

        foreach (BaseAction baseAction in baseActions) {
            GameObject buttonTransform = Instantiate(actionButtonPrefab, actionButtonsParent);
            ActionButtonUI actionButtonUI = buttonTransform.GetComponent<ActionButtonUI>();
            actionButtonsUIS.Add(actionButtonUI);
            actionButtonUI.SetBaseAction(baseAction);
        }
    }

    private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs empty) {
        CreateUnitActionButtons();
        UpdateSelectedVisual();
        UpdateActionPoints();
    }

    private void UnitActionSystem_OnSelectedActionChanged(object sender, EventArgs empty) {
        UpdateSelectedVisual();
    }

    private void UnitActionSystem_OnActionStarted(object sender, EventArgs empty) {
        UpdateActionPoints();
    }

    private void UpdateSelectedVisual() {
        foreach (ActionButtonUI actionButtonUI in actionButtonsUIS) {
            actionButtonUI.UpdateSelectedVisual();
        }
    }

    private void UpdateActionPoints() {
        Unit unit = UnitActionSystem.Instance.GetSelectedUnit();
        actionPointsText.text = "Action Points: " + unit.GetActionPoints();
    }
}