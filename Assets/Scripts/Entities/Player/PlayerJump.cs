using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(VerticalState), typeof(Rigidbody))]
class PlayerJump : MonoBehaviour
{
    private Rigidbody rigidbody;
    [SerializeField] private InputActionReference jumpAction;
    private VerticalState verticalState;
    public float jumpForce;


    private void Awake()
    {
        FetchDependencies();
    }

    private void FetchDependencies()
    {
        rigidbody = GetComponent<Rigidbody>();
        verticalState = GetComponent<VerticalState>();
    }

    private void Update()
    {
        QDebugManager.Instance.Trace(this, "pressed: " + jumpAction.action.IsPressed());
        if (jumpAction.action.IsPressed())
        {
            if (AbleToJump())
            {
                Jump();
            }
        }
    }

    public bool AbleToJump()
    {
        if (verticalState.CurrentVerticalState != EVerticalState.Grounded)
            return false;

        return true;    // If player is grounded return true
    }

    public void Jump()
    {
        QDebugManager.Instance.Mild(this, "Jumping");
        rigidbody.linearVelocity = new(0f, jumpForce, 0f);
    }
}
