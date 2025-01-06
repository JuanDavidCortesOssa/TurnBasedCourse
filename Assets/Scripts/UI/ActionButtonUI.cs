using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtonUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private Button button;
    [SerializeField] private Transform selectedVisual;
    private BaseAction _buttonAction;

    public void SetBaseAction(BaseAction action) {
        _buttonAction = action;
        textMesh.text = action.GetActionName().ToUpper();
        button.onClick.AddListener((() => { UnitActionSystem.Instance.SetSelectedAction(action); }));
    }

    public void UpdateSelectedVisual() {
        BaseAction currentSelectedAction = UnitActionSystem.Instance.GetSelectedAction();
        selectedVisual.gameObject.SetActive(_buttonAction == currentSelectedAction);
    }
}