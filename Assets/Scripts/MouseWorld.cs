using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    [SerializeField] private LayerMask floorLayerMask;

    public static MouseWorld Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        GetMousePosition();
    }

    private void GetMousePosition()
    {
        transform.position = GetPosition();
    }

    public static Vector3 GetPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        Physics.Raycast(ray, out RaycastHit raycastInfo, float.MaxValue, Instance.floorLayerMask);
        return raycastInfo.point;
    }
}
