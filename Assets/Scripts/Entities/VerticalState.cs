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
            QDebugManager.Instance.Mild(this, "CurrentVerticalState was set to " + value.ToString());
        }
    }

    [Header("Debug")]
    private EVerticalState previousVerticalState;

    private void Awake()
    {
        if (groundDetectorsParent != null && groundDetectors.Length == 0)
            groundDetectors = groundDetectorsParent.GetComponentsInChildren<Detector>();
    }

    private void FixedUpdate()
    {
        CurrentVerticalState = CalculateVerticalState();
        if (previousVerticalState != CurrentVerticalState)
        {
            QDebugManager.Instance.Verbose(this, "Current vertical state: " + CurrentVerticalState);
            previousVerticalState = CurrentVerticalState;
        }

        groundDetection = (CurrentVerticalState != EVerticalState.Grounded);
    }

    private EVerticalState CalculateVerticalState()
    {
        foreach (Detector groundDetector in groundDetectors)
        {
            // bool timeSinceLastCheckIsOutdated = groundDetector.timeSinceDisabled > groundDetector.timeSinceLastCheck;


            if (groundDetector.targetDetectedLastCheck && (groundDetector.timeSinceLastCheck <= Time.fixedDeltaTime) && CurrentVerticalState != EVerticalState.Jumping)
                return EVerticalState.Grounded;
        }

        float upwardVelocity = rigidBody.linearVelocity.y;
        // if (upwardVelocity >= floatingTolerance)
        //     return EVerticalState.Jumping;

        if (upwardVelocity <= -floatingTolerance)
            return EVerticalState.Falling;

        if (upwardVelocity >= -floatingTolerance && upwardVelocity <= floatingTolerance && currentVerticalState != EVerticalState.Grounded)
            return EVerticalState.Floating;     // beware! At the peak of the jump, before falling, the player will be considered to be floating. 

        return CurrentVerticalState;
    }

    public void GroundDetected()
    {
        GroundDetection = false;
    }

}
