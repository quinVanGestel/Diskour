using UnityEngine;

public class VerticalState : MonoBehaviour
{

    public Rigidbody rigidBody;

    [Tooltip("Pick one of the two :)")]
    public GameObject groundDetectorsParent;
    [Tooltip("Pick one of the two :)")]
    public Detector[] groundDetectors;
    private bool groundDetection;
    public bool GroundDetection
    {
        get { return groundDetection; }
        set
        {
            groundDetection = value;
            foreach (Detector groundDetector in groundDetectors)
            {
                groundDetector.gameObject.SetActive(groundDetection);
                QDebugManager.Instance.Verbose(this, "SetActive " + groundDetector.name + " is now " + groundDetection.ToString());
            }
        }
    }

    private EVerticalState currentVerticalState;
    /// <summary>
    /// Setting is only needed when setting it to grounded, the getter can handle the rest intrinsically.
    /// </summary>
    public EVerticalState CurrentVerticalState
    {
        get
        {
            if (rigidBody.linearVelocity.y >= 0.1f)
                return EVerticalState.Jumping;

            if (rigidBody.linearVelocity.y <= -0.1f)
                return EVerticalState.Falling;

            if (rigidBody.linearVelocity.y == 0f && currentVerticalState != EVerticalState.Grounded)   // beware! At the peak of the jump, before falling, the player will be considered to be floating. 
                return EVerticalState.Floating;

            return EVerticalState.Grounded;
        }
        set
        {
            if (currentVerticalState == value)
            {
                return;
            }
            currentVerticalState = value;
            QDebugManager.Instance.Mild(this, "CurrentVerticalState was set to " + value.ToString());
        }
    }

    private void Awake()
    {
        if (groundDetectorsParent != null && groundDetectors.Length == 0)
            groundDetectors = groundDetectorsParent.GetComponentsInChildren<Detector>();
    }

    public void GroundDetected()
    {
        if (CurrentVerticalState != EVerticalState.Jumping)
            CurrentVerticalState = EVerticalState.Grounded;
        GroundDetection = false;
    }

}