using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour {
    #region Singleton

    public static TurnSystem Instance;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There are two turn systems on the scene");
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    #endregion

    public event EventHandler OnTurnChanged;

    private int turnNumber = 1;

    public void NextTurn() {
        turnNumber++;
        OnTurnChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetTurnNumber() {
        return turnNumber;
    }
}