using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player : Entity
{
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference updateRotationAction;
    [Tooltip("on false the player will rotate when held, on true it will activate when not held")]
    public bool invertUpdateRotationAction;

    private Vector2 moveInput;
    private CharacterController characterController;
    [SerializeField] private Camera playerCamera;
    // private float totalVerticalForce;
    private float dragAppliedLastFrame;
    private float gravityAppliedLastFrame;
    private float gravityAcceleration = 9.81f;

    private void Awake()
    {
        Debug.Log("Running the awake function");
        characterController = gameObject.GetComponent<CharacterController>();
        if (characterController == null)
        {
            if (debugLog) Debug.Log("Failed to fetch character controller");
        }
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
    }

    private float IncrementGravity()
    {

        return -1;
    }

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
        Debug.Log("IsPressed: \n" + updateRotationAction.action.IsPressed());
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
        characterController.Move(moveDirection);
    }

}
