using UnityEngine;
using UnityEngine.Events;

public class Detector : MonoBehaviour
{
    [Header("On")]
    public bool triggerEnter;
    public bool collisionEnter;

    [Header("If")]
    public string[] otherTags;
    public string[] otherNames;
    public Component[] otherClass;

    [Header("Then")]
    public UnityEvent action;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggerEnter)
        {
            return;
        }

        bool tagMatches;
        foreach (string otherTag in otherTags)
        {
            if (other.CompareTag(otherTag))
            {
                tagMatches = true;
            }
        }

        bool nameMatches;
        if (otherNames.Length == 0)
        {
            nameMatches = true;
        }
        else
        {
            foreach (string otherName in otherNames)
            {
                if (other.name == otherName)
                {
                    nameMatches = true;
                }
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {

    }




}
