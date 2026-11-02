using System;
using System.Xml;
using UnityEngine;

[RequireComponent(typeof(VerticalState))]

public abstract class Entity : MonoBehaviour, ICanDie
{
    public Transform spawnAnchor;
    public bool debugLog;

    private void Update()
    {
        OutOfBoundsCheck();
    }

    /// <summary>
    /// Currently only checking negative y, aren't I so naughty 🤭
    /// </summary>
    private void OutOfBoundsCheck()
    {
        bool outOfBounds = transform.position.y < GameManager.Instance.boundsLower.y;
        if (!outOfBounds)
        {
            return;
        }

        Kill();
        transform.position.Set(transform.position.x, GameManager.Instance.boundsLower.y, transform.position.z); // To make sure it doesn't overshoot.
    }

    public abstract void Kill();

}
