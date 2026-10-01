using System;
using UnityEngine;

public enum EVerticalState
{
    Falling, Grounded, Jumping, Floating
}

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
                groundDetector.DetectionEnabled = groundDetection;
                QDebugManager.Instance.Trace(this, "DetectionEnabled is now " + groundDetection.ToString());
            }
        }
    }


    [Tooltip("How high a rigidbody's velocity can be before it is no longer considered floating.")]
    public float floatingTolerance;

    private EVerticalState currentVerticalState;
    /// <summary>
    /// Setting is only needed when setting it to grounded, the getter can handle the rest intrinsically.
    /// </summary>
    public EVerticalState CurrentVerticalState
    {
        get
        {
            return currentVerticalState;
        }
        set
        {
            if (currentVerticalState == value)
            {
                return;
            }
            currentVerticalState = value;
            QDebugManager.Instance.Trace(this, "CurrentVerticalState was set to " + value.ToString());
        }
    }


    [Header("Debug")]
    [SerializeField] private EVerticalState currentVerticalStateReadOnly;
    private EVerticalState previousVerticalState;

    private void Awake()
    {
        if (groundDetectorsParent != null && groundDetectors.Length == 0)
            groundDetectors = groundDetectorsParent.GetComponentsInChildren<Detector>();
    }

    private void FixedUpdate()
    {
        CurrentVerticalState = CalculateVerticalState();
        currentVerticalStateReadOnly = CurrentVerticalState;
        if (previousVerticalState != CurrentVerticalState)
        {
            QDebugManager.Instance.Verbose(this, "Current vertical state: " + CurrentVerticalState);
            previousVerticalState = CurrentVerticalState;
        }

    }

    private void Update()
    {
        GroundDetection = CurrentVerticalState != EVerticalState.Grounded;  // If it's grounded it won't detect and vice versa
    }

    private EVerticalState CalculateVerticalState()
    {

        float upwardVelocity = rigidBody.linearVelocity.y;
        if (upwardVelocity >= floatingTolerance)
            return EVerticalState.Jumping;

        if (upwardVelocity <= -floatingTolerance)
            return EVerticalState.Falling;

        if (upwardVelocity >= -floatingTolerance && upwardVelocity <= floatingTolerance && currentVerticalState != EVerticalState.Grounded)
            return EVerticalState.Floating;     // beware! At the peak of the jump, before falling, the player will be considered to be floating. 


        // foreach (Detector groundDetector in groundDetectors)
        // {
        //     // bool timeSinceLastCheckIsOutdated = groundDetector.timeSinceDisabled > groundDetector.timeSinceLastCheck;



        //     if (groundDetector.targetDetectedLastCheck && (groundDetector.timeSinceLastCheck <= Time.fixedDeltaTime) && CurrentVerticalState != EVerticalState.Jumping)
        //         return EVerticalState.Grounded;
        // }


        return CurrentVerticalState;
    }

    public void GroundDetected()
    {
        CurrentVerticalState = EVerticalState.Grounded; // Assume it's grounded
        CurrentVerticalState = CalculateVerticalState();// Until proven otherwise
        GroundDetection = CurrentVerticalState != EVerticalState.Grounded;  // If it's grounded it won't detect and vice versa
    }

}
