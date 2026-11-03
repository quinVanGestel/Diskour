using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHorizontalMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference moveAction;

    [Tooltip("Meters per second")]
    [SerializeField] private float speed;
    public float Speed => speed;


    private void Awake()
    {
        FetchDependencies();
    }

    private void FetchDependencies()
    {
        // Stub :D
    }

    private void Update()
    {
        HandlePlayerMovement();
    }

    private void HandlePlayerMovement()
    {
        QDebugManager.Instance.Trace(this, "pressed: " + moveAction.action.IsPressed());
        if (moveAction.action.IsPressed())
        {
            MovePlayer(moveAction.action.ReadValue<Vector2>());            
        }
    }

    private void MovePlayer(Vector2 moveInput)
    {
        Vector2 vector2Right = new(transform.right.x, transform.right.z);
        QDebugManager.Instance.Trace(this, "vector2Right: " + vector2Right);
        Vector2 vector2Forward = new(transform.forward.x, transform.forward.z);
        QDebugManager.Instance.Trace(this, "vector2Forward: " + vector2Forward);

        Vector2 directionAdjustedMoveInput = moveInput.x * vector2Right.normalized + moveInput.y * vector2Forward.normalized;
        QDebugManager.Instance.Trace(this, "directionAdjustedMoveInput: " + directionAdjustedMoveInput);

        Vector2 speedAdjustedMoveInput = directionAdjustedMoveInput.normalized * (Time.deltaTime * speed);
        QDebugManager.Instance.Trace(this, "speedAdjustedMoveInput: " + speedAdjustedMoveInput);

        Vector3 moveDirectionVector3 = new(speedAdjustedMoveInput.x, 0f, speedAdjustedMoveInput.y);
        QDebugManager.Instance.Trace(this, "moveDirectionVector3: " + moveDirectionVector3);

        Vector3 currentPositionVector3 = transform.position;
        QDebugManager.Instance.Trace(this, "currentPositionVector3: " + currentPositionVector3);

        Vector3 newPositionVector3 = currentPositionVector3 + moveDirectionVector3;
        QDebugManager.Instance.Trace(this, "newPositionVector3: " + newPositionVector3);

        // rigidBody.AddForce(moveDirectionVector3);
        transform.position = newPositionVector3;
    }

}
