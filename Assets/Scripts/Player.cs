using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player : Entity
{
    [SerializeField] private InputActionReference moveAction;
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

    private float IncrementGravity()
    {

        return -1;
    }

    private void HandlePlayerMovement()
    {
        moveInput = moveAction.action.ReadValue<Vector2>();

        Quaternion compensatedRotation = playerCamera.transform.rotation *
          gameObject.transform.rotation;
        Debug.Log("compensatedRotation: " + compensatedRotation);

        Debug.Log("moveinput: " + moveInput);
        Vector2 camera2dForward = new(playerCamera.transform.forward.x, playerCamera.transform.forward.z);
        Vector2 camera2dRight = new(playerCamera.transform.right.x, playerCamera.transform.right.z);
        Vector2 camera2d = new(playerCamera.transform.localEulerAngles.x, playerCamera.transform.localEulerAngles.y);
        Debug.Log("camera2d: " + camera2d);

        Debug.Log("camera2dforward: " + camera2dForward);
        Vector2 compensatedMoveInput = moveInput.x * camera2dRight.normalized + moveInput.y * camera2dForward.normalized;
        Vector2 speedAdjustedMoveInput = compensatedMoveInput.normalized * (Time.fixedDeltaTime * speed);
        Debug.Log("speedAdjustedMoveInput: " + speedAdjustedMoveInput);
        Debug.Log("compensatedMoveInput: " + compensatedMoveInput);
        Vector3 moveDirection = new(speedAdjustedMoveInput.x, 0, speedAdjustedMoveInput.y);

        Debug.Log("movedirection: " + moveDirection);
        characterController.Move(moveDirection);
    }

}
