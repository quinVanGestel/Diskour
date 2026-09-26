using UnityEngine;




/// <summary>
/// deprecated
/// </summary>
public class GravityHandler : MonoBehaviour
{
    // #region Gravity
    // [Header("Gravity")]
    // public float startGravitySpeed;
    // public float maximumGravitySpeed;
    // public float gravityAcceleration;
    // public float currentGravitySpeed;
    // public Transform gravityRayCastAnchor;
    // public MeshFilter meshFilter;
    // public bool grounded;
    // private float radius;
    // // private Vector3 centre;
    // public float groundDistance;
    // #endregion

    // [Header("VerticalState")]
    // private EVerticalState currentVerticalState;
    // public EVerticalState CurrentVerticalState
    // {
    //     get { return currentVerticalState; }
    //     set
    //     {
    //         if (currentVerticalState == value)
    //         {
    //             return;
    //         }
    //         currentVerticalState = value;
    //         if (debugLog) Debug.Log("CurrentVerticalState was set to \n" + value.ToString());
    //     }
    // }

    // [Header("Debug")]
    // public bool debugLog;
    // private void Awake()
    // {
    //     radius = meshFilter.mesh.bounds.extents.x;
    //     // centre = meshFilter.mesh.bounds.center;
    // }

    // private void Update()
    // {
    //     // Debug.Log(IsGrounded());
    //     // Debug.Log(CurrentVerticalState);
    //     // Debug.Log(characterController.isGrounded);
    //     // Vector3 rayDirection = (transform.up * -1f);
    //     // Debug.DrawRay(transform.position, rayDirection);
    //     // Debug.Log(rayDirection);
    // }

    // private void OnCollisionEnter(Collision collision)
    // {
    //     if (!collision.gameObject.CompareTag("Ground"))
    //     {
    //         return;
    //     }
    //     CurrentVerticalState = EVerticalState.Grounded;
    // }

    // private void OnCollisionExit(Collision collision)
    // {
    //     if (!collision.gameObject.CompareTag("Ground"))
    //         return;

    //     if (CurrentVerticalState != EVerticalState.Jumping)
    //     {
    //         CurrentVerticalState = EVerticalState.Falling;
    //     }
    // }

    // // public bool IsGrounded()
    // // {

    // // }

    // // private float GetRadius(){

    // // }

    // // private bool IsGrounded()
    // // {
    // //     Vector3 rayDirection = (transform.up * -1f);
    // //     if (Physics.SphereCast(transform.position, radius, direction:rayDirection, out RaycastHit hitInfo, groundDistance))
    // //     {
    // //         if (hitInfo.collider.gameObject.CompareTag("Ground"))
    // //             return true;
    // //     }

    // //     return false;
    // // }

}