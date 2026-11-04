using UnityEngine;
using UnityEngine.InputSystem;

class PlayerCameraHandler : MonoBehaviour
{
    [SerializeField] private InputActionReference updateRotationAction;
    [Tooltip("on false the player will rotate when held, on true it will activate when not held")]
    public bool rotatePlayerOnHold;

    private void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        transform.rotation = CalculatePlayerRotation();
    }

    private Quaternion CalculatePlayerRotation()
    {
        Vector3 currentRotation = new(0f, transform.rotation.eulerAngles.y, 0f);
        bool rotateButtonIsHeld = updateRotationAction.action.IsPressed();

        if ((rotatePlayerOnHold && !rotateButtonIsHeld)    // If the button needs to be held but the button is not held
        || (!rotatePlayerOnHold && rotateButtonIsHeld))    // or if the button needs to be released but the button is held
        {
            return Quaternion.Euler(currentRotation);    // Return old y rotation.
        }

        Vector3 newRotation = currentRotation;
        newRotation.y = GameState.Instance.PlayerCamera.transform.rotation.eulerAngles.y;   // Make the y rotation follow the camera.
        return Quaternion.Euler(newRotation);
    }

}
