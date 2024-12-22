using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MoveAction : MonoBehaviour {
    [SerializeField] private Animator unitAnimator;
    private Vector3 targetPosition;
    private float moveSpeed = 4f;
    private Unit unit;

    [FormerlySerializedAs("maxMovementRange")] [SerializeField]
    private int maxMoveDistance = 4;

    private void Awake() {
        targetPosition = transform.position;
        unit = GetComponent<Unit>();
    }

    void Update() {
        float stoppingDistance = .1f;
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance) {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            transform.position += moveDirection * Time.deltaTime * moveSpeed;
            unitAnimator.SetBool("IsWalking", true);

            float rotateSpeed = 10f;
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
        } else {
            unitAnimator.SetBool("IsWalking", false);
        }
    }

    public void Move(Vector3 targetPosition) {
        this.targetPosition = targetPosition;
    }

    public List<GridPosition> GetValidGridPositionList() {
        List<GridPosition> gridPositions = new List<GridPosition>();

        GridPosition currentGridPosition = unit.GetCurrentGridPosition();

        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++) {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++) {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition
                    testPosition = offsetGridPosition + currentGridPosition; //Add the current position of the character
                Debug.Log(testPosition);
            }
        }

        return gridPositions;
    }
}