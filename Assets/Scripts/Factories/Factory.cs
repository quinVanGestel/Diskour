// Stolen with some modifications. Credit: Level_up_your_code_with_game_programming_patterns___Unity
using UnityEngine;


public abstract class Factory : MonoBehaviour
{
    [SerializeField] protected GameObject prefab;

    /// <summary>
    /// Create an instance of the prefab in this factory.
    /// </summary>
    /// <param name="position">Where to spawn the product</param>
    /// <returns>Get the IProduct component of the instance</returns>
    public abstract IProduct SpawnProduct(Vector3 position);
    
}
