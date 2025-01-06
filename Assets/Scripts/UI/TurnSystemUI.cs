using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class TurnSystemUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI turnText;
    [SerializeField] private Button endTurnButton;

    private void Start() {
        endTurnButton.onClick.AddListener(() => { TurnSystem.Instance.NextTurn(); });
        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;

        UpdateTurnText();
    }

    private void TurnSystem_OnTurnChanged(object obj, EventArgs e) {
        UpdateTurnText();
    }

    private void UpdateTurnText() {
        turnText.text = "TURN " + TurnSystem.Instance.GetTurnNumber();
    }
}