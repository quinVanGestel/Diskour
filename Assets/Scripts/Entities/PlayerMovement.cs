using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public VerticalState verticalState;
    public Rigidbody rigidBody;
    public Camera playerCamera;

    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;
    [SerializeField] private InputActionReference updateRotationAction;
    [Tooltip("on false the player will rotate when held, on true it will activate when not held")]
    public bool invertUpdateRotationAction;

    [Tooltip("Meters per second")]
    [SerializeField] private float speed;
    public float Speed => speed;
    public float jumpForce;

    // [Tooltip("If the player's vertical velocity is above this value the player will be considered jumping. Uses the same unit as the jumpForce variable.")]
    // public float jumpForceDetectionTolerance;    // Redundant. VerticalState already does this with floatingTolerance.


    private void Update()
    {
        HandlePlayerMovement();

        transform.rotation = CalculatePlayerRotation();
        // QDebugManager.Instance.Trace(this, "pressed: " + jumpAction.action.IsPressed());
        if (jumpAction.action.IsPressed())
        {
            if (AbleToJump())
                Jump();
        }
    }


    public bool AbleToJump()
    {
        if (verticalState.CurrentVerticalState != EVerticalState.Grounded)
            return false;

        // if (rigidBody.linearVelocity.y >= jumpForceDetectionTolerance * 100f)
        //     return false;

        return true;    // If player is grounded return true
    }

    public void Jump()
    {
        QDebugManager.Instance.Mild(this, "Jumping");
        rigidBody.linearVelocity = new(0f, jumpForce, 0f);
        // verticalState.CurrentVerticalState = EVerticalState.Jumping;
        // verticalState.GroundDetection = true;
    }

    private Quaternion CalculatePlayerRotation()
    {
        Vector3 newRotationVector3 = new(0f, transform.rotation.eulerAngles.y, 0f);
        if ((!invertUpdateRotationAction && !updateRotationAction.action.IsPressed())    // If the button needs to be held but the button is not held
        || (invertUpdateRotationAction && updateRotationAction.action.IsPressed()))    // or if the button needs to be released but the button is held
            return Quaternion.Euler(newRotationVector3);    // Return old y rotation.

        newRotationVector3.y = playerCamera.transform.rotation.eulerAngles.y;   // Make the y rotation follow the camera.
        return Quaternion.Euler(newRotationVector3);
    }

    private void HandlePlayerMovement()
    {
        // QDebugManager.Instance.Trace(this, "pressed: " + moveAction.action.IsPressed());
        if (moveAction.action.IsPressed())
            MovePlayer(moveAction.action.ReadValue<Vector2>());
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
        // Vector3 newPositionVector3 = new(newPositionVector2.x, transform.position.y, newPositionVector2.y);
        QDebugManager.Instance.Trace(this, "newPositionVector3: " + newPositionVector3);

        // rigidBody.AddForce(moveDirectionVector3);
        transform.position = newPositionVector3;
    }

}
