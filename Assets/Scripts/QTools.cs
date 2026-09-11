using UnityEngine;
using System.Collections.Generic;
using System;

public static class QTools
{

    /// <returns>Returns the array index of the item if it can be found in the array. Returns -1 otherwise.</returns>
    public static int FindIndex<T>(T item, T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (item.Equals(array[i]))
                return i;
        }
        return -1;
    }

    /// <summary>
    /// Compares each number in array one to each number in array two. Finds the pair between which the distance is the largest. Returns that distance.
    /// </summary>
    /// <param name="numbersArrayOne"></param>
    /// <param name="numbersArrayTwo"></param>
    /// <returns></returns>
    public static float DistanceLargest(float[] numbersArrayOne, float[] numbersArrayTwo)
    {
        float largestDistanceSoFar = numbersArrayOne[0];
        foreach (float i in numbersArrayOne)
        {
            foreach (float j in numbersArrayTwo)
            {
                if (i - j > largestDistanceSoFar)
                {
                    largestDistanceSoFar = i - j;
                }
            }
        }

        return largestDistanceSoFar;
    }

    /// <summary>
    /// Compares each number in array one to each number in array two. Finds the pair between which the distance is the smallest. Returns that distance.
    /// </summary>
    /// <param name="numbersArrayOne"></param>
    /// <param name="numbersArrayTwo"></param>
    /// <returns></returns>
    public static float DistanceSmallest(float[] numbersArrayOne, float[] numbersArrayTwo)
    {
        float smallestDistanceSoFar = numbersArrayOne[0];
        foreach (float i in numbersArrayOne)
        {
            foreach (float j in numbersArrayTwo)
            {
                if (i - j < smallestDistanceSoFar)
                {
                    smallestDistanceSoFar = i - j;
                }
            }
        }

        return smallestDistanceSoFar;
    }

    /// <summary>
    /// Returns all children of the given gameobject, looks only one layer deep.
    /// </summary>
    /// <param name="parentTransform"></param>
    /// <returns></returns>
    public static GameObject[] GetAllChildren(GameObject parent)
    {
        List<GameObject> children = new();
        foreach (Transform child in parent.transform)
        {
            children.Add(child.gameObject);
        }

        return children.ToArray();
    }


    /// <summary>
    /// Returns a component of the requested type from the gameobject siblings of the given gameobject.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    public static T GetComponentInSiblings<T>(GameObject gameObject)
    {
        return gameObject.transform.parent.gameObject.GetComponentInChildren<T>();
    }

    /// <summary>
    /// Returns all components of the requested type from the gameobject siblings of the given gameobject.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    public static T[] GetComponentsInSiblings<T>(GameObject gameObject)
    {
        return gameObject.transform.parent.gameObject.GetComponentsInChildren<T>();
    }

    // /// <summary>
    // /// I recommend using the resizer script where possible, as this allows for variable storage.
    // /// </summary>
    // /// <param name="newSize"></param>
    // /// <param name="resizeMode"></param>
    // /// <param name="gameObject"></param>
    // /// <param name="currentSize"></param>
    // /// <returns></returns>
    // public static Vector3 SetSize(Vector3 newSize, ResizeModes resizeMode, GameObject gameObject, Vector3 currentSize)
    // {
    //     Vector3 rescaleFactor = gameObject.transform.localScale;

    //     switch (resizeMode)
    //     {
    //         case ResizeModes.Stretch:
    //             rescaleFactor.x = newSize.x * rescaleFactor.x / currentSize.x;
    //             rescaleFactor.y = newSize.y * rescaleFactor.y / currentSize.y;
    //             rescaleFactor.z = newSize.z * rescaleFactor.z / currentSize.z;
    //             break;
    //     }

    //     return rescaleFactor;
    // }

    /// <returns>Either -1 or 1</returns>
    public static int RandomDirection1D()
    {
        if (UnityEngine.Random.Range(0, 2) == 1) return 1;
        else return -1;
    }

    //A beautiful function I definitely did not steal :3
    /// <summary>
    /// 
    /// </summary>
    /// <param name="target">Mean of the distribution</param>
    /// <param name="minInclusive"></param>
    /// <param name="maxInclusive"></param>
    /// <param name="standardDeviation">Standard deviation controls the spread of values</param>
    /// <returns></returns>
    public static float GenerateRandomNumberNearTarget(float target, float minInclusive, float maxInclusive, float standardDeviation)
    {
        System.Random random = new();
        float mean = target;
        double u1 = 1.0 - random.NextDouble();
        double u2 = 1.0 - random.NextDouble();
        double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
        double randNormal = mean + standardDeviation * randStdNormal;
        float generatedNumber = (float)Math.Clamp(randNormal, minInclusive, maxInclusive); // Ensure the generated value is within the specified range
        return generatedNumber;
    }

    public static bool AnyStringMatches(string[] primaryStrings, string[] secondaryStrings)
    {
        foreach (string primaryString in primaryStrings)
        {
            foreach (string secondaryString in secondaryStrings)
            {
                if (primaryString == secondaryString)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public static bool AnyMonoBehaviourMatches(Component[] primaryArray, Component[] secondaryArray)
    {
        foreach (Component primaryItem in primaryArray)
        {
            foreach (Component secondaryItem in secondaryArray)
            {
                // Debug.Log("Comparing " + primaryItem.name + " against " + secondaryItem.name);
                if (primaryItem.GetType() == secondaryItem.GetType())
                {
                    Debug.Log(primaryItem.name + " equals " + secondaryItem.name);
                    return true;
                }
            }
        }
        // Debug.Log("None of the components match.");
        return false;
    }

}
