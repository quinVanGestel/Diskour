using System;
using System.Drawing;
using UnityEngine;

public class PlayerCorpseFactory : Factory
{

    #region Singleton
    private static PlayerCorpseFactory instance;
    public static PlayerCorpseFactory Instance => instance;

    private void SingletonSetup()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        instance = this;
    }
    #endregion

    [Tooltip("1,1,1 means nothing happens, between 0 and 1 shrinks, above 1 grows.")]
    [SerializeField] private Vector3 sizeMultiplierOnIteration = new(1f, 1f, 1f);

    private void Awake()
    {
        SingletonSetup();
    }

    public override IProduct SpawnProduct(Vector3 position)
    {
        GameObject prefabInstance = Instantiate(prefab,
        position, Quaternion.identity); // Spawn player

        ShrinkPlayer(prefabInstance.transform); // Shrink player

        GameState.Instance.IterationCount++;    // Log it
        GameState.Instance.PlayerReference = prefabInstance.GetComponent<Player>(); // Log it


        IProduct newProduct = prefabInstance.GetComponent<IProduct>();  // Grab the product part of the player (that allows it to be birthed)
        newProduct.Initialize();    // Tell player it has been born

        return newProduct;
    }

    private void ShrinkPlayer(Transform playerTransform)
    {
        Vector3 newSize = QTools.newSizeExponential(playerTransform.localScale, sizeMultiplierOnIteration, GameState.Instance.IterationCount);
        playerTransform.localScale = newSize;
    }

}
