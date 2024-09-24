using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    GridSystem gridSystem;

    public void Start()
    {
        gridSystem = new(10 , 10, 2f);
        Debug.Log("Created grid");
    }

    public void Update()
    {
        Debug.Log(gridSystem.GetGridPosition(MouseWorld.GetPosition()));
    }
}