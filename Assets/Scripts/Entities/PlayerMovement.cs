using UnityEngine;
using UnityEngine.InputSystem;

public enum EVerticalState
{
    Falling = 0, Grounded, Jumping
}

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;
    [SerializeField] private InputActionReference updateRotationAction;
    [Tooltip("on false the player will rotate when held, on true it will activate when not held")]
    public bool invertUpdateRotationAction;
    private EVerticalState currentVerticalState;
    public EVerticalState CurrentVerticalState
    {
        get { return currentVerticalState; }
        set
        {
            if (currentVerticalState == value)
            {
                return;
            }
            currentVerticalState = value;
            if (debugLog) Debug.Log("CurrentVerticalState was set to \n" + value.ToString());
        }
    }
    public Rigidbody rigidBody;
    [Tooltip("Meters per second")]
    public float speed;
    private Vector2 moveInput;
    [SerializeField] private Camera playerCamera;
    public bool jumping;
    public bool debugLog;

    // private float totalVerticalForce;
    // private float dragAppliedLastFrame;
    // private float gravityAppliedLastFrame;
    // private float gravityAcceleration = 9.81f;

    private void Awake()
    {
        Debug.Log("Running the awake function");
        // characterController = gameObject.GetComponent<CharacterController>();
        // if (characterController == null)
        // {
        //     if (debugLog) Debug.Log("Failed to fetch character controller");
        // }
        if (moveAction == null)
        {
            Debug.Log("move action null");
        }

    }

    private void FixedUpdate()
    {
        // Debug.Log("pressed " + moveAction.action.IsPressed());
        if (moveAction.action.IsPressed())
            HandlePlayerMovement();


    }

    private void Update()
    {
        UpdatePlayerRotation();
        if (moveAction.action.WasPerformedThisFrame())
        {
            if (debugLog) Debug.Log("Player triggered jump");
            if (CurrentVerticalState == EVerticalState.Grounded)
                Jump();
        }
    }

    public void GroundDetected()
    {
// stub
    }

    private void Jump()
    {

    }

    // public bool AbleToJump()
    //     {

    //     }


    private EVerticalState CalculateVerticalState()
    {
        // calculate vertical state 😀👍

        return EVerticalState.Grounded;
    }

    private void Meow()
    {
        Debug.Log(" mrrrp :3c");
    }

    // private float IncrementGravity()
    // {
    //     startGravitySpeed
    //     return -1;
    // }



    // private Vector2 Camera2dForward()
    // {
    //     return new(playerCamera.transform.forward.x, playerCamera.transform.forward.z);
    // }

    // private Vector2 Camera2dRight()
    // {
    //     return new(playerCamera.transform.right.x, playerCamera.transform.right.z);
    // }

    private void UpdatePlayerRotation()
    {

        if ((!invertUpdateRotationAction && !updateRotationAction.action.IsPressed())    // If the button needs to be held but the button is not held
        || (invertUpdateRotationAction && updateRotationAction.action.IsPressed()))    // or if the button needs to be released but the button is held
            return;
        // InputAction rotateActionAction = updateRotationAction.action;
        // Debug.Log("IsPressed: \n" + updateRotationAction.action.IsPressed());
        // Debug.Log("WasPressedThisFrame: \n" + rotateActionAction.WasPressedThisFrame());
        // Debug.Log("WasPressedThisDynamicUpdate: \n" + rotateActionAction.WasPressedThisDynamicUpdate());
        // Debug.Log("WasPerformedThisFrame: \n" + rotateActionAction.WasPerformedThisFrame());
        // Debug.Log("WasPerformedThisDynamicUpdate: \n" + rotateActionAction.WasPerformedThisDynamicUpdate());
        // Debug.Log("WasReleasedThisFrame: \n" + rotateActionAction.WasReleasedThisFrame());
        // Debug.Log("WasReleasedThisDynamicUpdate: \n" + rotateActionAction.WasReleasedThisDynamicUpdate());
        // Debug.Log("WasCompletedThisFrame: \n" + rotateActionAction.WasCompletedThisFrame());
        // Debug.Log("WasCompletedThisDynamicUpdate: \n" + rotateActionAction.WasCompletedThisDynamicUpdate());
        transform.rotation = Quaternion.Euler(transform.rotation.x, playerCamera.transform.rotation.eulerAngles.y, transform.rotation.z);
    }

    private void HandlePlayerMovement()
    {
        moveInput = moveAction.action.ReadValue<Vector2>();

        // Quaternion compensatedRotation = playerCamera.transform.rotation *
        //   gameObject.transform.rotation;
        // Debug.Log("compensatedRotation: " + compensatedRotation);

        // Debug.Log("moveinput: " + moveInput);
        // Vector2 camera2d = new(playerCamera.transform.localEulerAngles.x, playerCamera.transform.localEulerAngles.y);
        // Debug.Log("camera2d: " + camera2d);

        // Debug.Log("camera2dforward: " + Camera2dForward());
        // Vector3 vector3RightNormalised = new(transform.right.x, transform.right.z);
        Vector2 vector2Right = new(transform.right.x, transform.right.z);
        Vector2 vector2Forward = new(transform.forward.x, transform.forward.z);
        Vector2 compensatedMoveInput = moveInput.x * vector2Right.normalized + moveInput.y * vector2Forward.normalized;
        Vector2 speedAdjustedMoveInput = compensatedMoveInput.normalized * (Time.fixedDeltaTime * speed);
        // Debug.Log("speedAdjustedMoveInput: " + speedAdjustedMoveInput);
        // Debug.Log("compensatedMoveInput: " + compensatedMoveInput);
        Vector3 moveDirection = new(speedAdjustedMoveInput.x, 0, speedAdjustedMoveInput.y);

        // Debug.Log("movedirection: " + moveDirection);

        Vector3 currentPosition = transform.position;
        Vector3 newPosition = currentPosition + moveDirection;
        transform.position = newPosition;
        // Debug.Log("currentPosition: " + currentPosition);
        // Debug.Log("newPosition: " + newPosition);

        // characterController.Move(moveDirection);
    }
}
