using System.Xml;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Tooltip("Meters per second")]
    [SerializeField] protected float speed;
    // [SerializeField] protected float mass;
    [Tooltip("")]
    public float startGravitySpeed;
    public float maximumGravitySpeed;
    public float gravityAcceleration;
    [SerializeField] protected bool debugLog;
}
