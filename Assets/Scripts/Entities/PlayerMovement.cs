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
    [Tooltip("If the player's vertical velocity is above this value the player will be considered jumping. Uses the same unit as the jumpForce variable.")]
    public float jumpForceDetectionTolerance;
    

    private void Update()
    {
        HandlePlayerMovement();

        UpdatePlayerRotation();
        QDebugManager.Instance.Trace(this, "pressed: " + jumpAction.action.IsPressed());
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

        if (rigidBody.linearVelocity.y >= jumpForceDetectionTolerance * 100f)
            return false;

        return true;    // If player is grounded return true
    }

    public void Jump()
    {
        QDebugManager.Instance.Mild(this, "Jumping");
        rigidBody.linearVelocity.Set(0f, jumpForce * 100f, 0f); // for some reason this thing needs some real high numbers to do anything interesting, so to compensate I * 100f
        verticalState.CurrentVerticalState = EVerticalState.Jumping;
        verticalState.GroundDetection = true;
    }

    private void UpdatePlayerRotation()
    {
        if ((!invertUpdateRotationAction && !updateRotationAction.action.IsPressed())    // If the button needs to be held but the button is not held
        || (invertUpdateRotationAction && updateRotationAction.action.IsPressed()))    // or if the button needs to be released but the button is held
            return;

        transform.rotation = Quaternion.Euler(transform.rotation.x, playerCamera.transform.rotation.eulerAngles.y, transform.rotation.z);
    }

    private void HandlePlayerMovement()
    {
        QDebugManager.Instance.Trace(this, "pressed: " + moveAction.action.IsPressed());
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

        Vector3 moveDirection = new(speedAdjustedMoveInput.x, 0, speedAdjustedMoveInput.y);
        QDebugManager.Instance.Trace(this, "movedirection: " + moveDirection);
        Vector3 currentPosition = transform.position;
        QDebugManager.Instance.Trace(this, "currentPosition: " + currentPosition);

        Vector3 newPosition = currentPosition + moveDirection;
        QDebugManager.Instance.Trace(this, "newPosition: " + newPosition);

        transform.position = newPosition;
    }

}
