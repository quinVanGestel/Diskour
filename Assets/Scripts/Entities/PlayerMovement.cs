using UnityEngine;
using UnityEngine.InputSystem;

public enum EVerticalState
{
    Falling = 0, Grounded, Jumping, Floating
}

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;
    [SerializeField] private InputActionReference updateRotationAction;
    [Tooltip("on false the player will rotate when held, on true it will activate when not held")]
    public bool invertUpdateRotationAction;
    public VerticalState verticalState;
    public Rigidbody rigidBody;
    [Tooltip("Meters per second")]
    public float speed;
    private Vector2 moveInput;
    [SerializeField] private Camera playerCamera;
    public bool jumping;
    public float jumpForce;
    public bool debugLog;

    private void Start()
    {
        QDebugManager.Instance.Verbose(this, "Running the awake function");
        if (moveAction == null)
        {
            Debug.Log("move action null");
        }

    }

    private void FixedUpdate()
    {
        QDebugManager.Instance.Trace(this, "pressed: " + moveAction.action.IsPressed());
        if (moveAction.action.IsPressed())
            HandlePlayerMovement();


    }

    private void Update()
    {
        UpdatePlayerRotation();
        QDebugManager.Instance.Trace(this, "pressed: " + jumpAction.action.IsPressed());
        if (jumpAction.action.IsPressed())
        {
            if (AbleToJump())
                Jump();
        }
    }

    public void Jump()
    {
        QDebugManager.Instance.Mild(this, "Jumping");
        rigidBody.AddForce(0f, jumpForce * 100f, 0f); // for some reason this thing needs some real high numbers to do anything interesting, so to compensate I * 100f
        verticalState.CurrentVerticalState = EVerticalState.Jumping;
        verticalState.GroundDetection = true;
    }

    public bool AbleToJump()
    {
        if (verticalState.CurrentVerticalState != EVerticalState.Grounded)
        {
            return false;
        }

        return true;
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
        moveInput = moveAction.action.ReadValue<Vector2>();

        Vector2 vector2Right = new(transform.right.x, transform.right.z);
        QDebugManager.Instance.Trace(this, "vector2Right: " + vector2Right);
        Vector2 vector2Forward = new(transform.forward.x, transform.forward.z);
        QDebugManager.Instance.Trace(this, "vector2Forward: " + vector2Forward);

        Vector2 directionAdjustedMoveInput = moveInput.x * vector2Right.normalized + moveInput.y * vector2Forward.normalized;
        QDebugManager.Instance.Trace(this, "directionAdjustedMoveInput: " + directionAdjustedMoveInput);

        Vector2 speedAdjustedMoveInput = directionAdjustedMoveInput.normalized * (Time.fixedDeltaTime * speed);
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
