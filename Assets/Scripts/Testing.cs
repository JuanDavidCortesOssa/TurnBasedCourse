using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    GridSystem gridSystem;
    [SerializeField] private Transform debugObject;

    public void Start()
    {
        gridSystem = new(10 , 10, 2f);
        gridSystem.CreateDebugObjects(debugObject);

        Debug.Log("Created grid");
    }

    public void Update()
    {
        Debug.Log(gridSystem.GetGridPosition(MouseWorld.GetPosition()));
    }
}