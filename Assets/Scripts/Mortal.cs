using System.Xml;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Tooltip("Meters per second")]
    [SerializeField] protected float speed;
    [SerializeField] protected float mass;
    [SerializeField] protected bool gravity;
    [SerializeField] protected bool debugLog;
}
