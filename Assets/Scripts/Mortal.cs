using System;
using System.Xml;
using UnityEngine;

public class Entity : MonoBehaviour
{

    public enum VerticalState
    {
        Falling = 0, Grounded, Jumping, Floating
    }

    [Tooltip("Meters per second")]
    [SerializeField] protected float speed;
    // [SerializeField] protected float mass;
    [Tooltip("")]
    public float startGravitySpeed;
    public float maximumGravitySpeed;
    public float gravityAcceleration;
    public float currentGravitySpeed;
    [SerializeField] protected bool debugLog;
}
