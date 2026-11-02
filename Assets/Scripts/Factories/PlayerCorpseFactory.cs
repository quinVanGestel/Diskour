using System;
using System.Drawing;
using UnityEngine;

public class PlayerCorpseFactory : Factory
{

    #region Singleton
    private static PlayerCorpseFactory instance;
    public static PlayerCorpseFactory Instance
    {
        get
        {
            return instance;
        }
    }

    private void SingletonSetup()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
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
        position, Quaternion.identity);

        GameState.Instance.IterationCount++;
        ShrinkPlayer(prefabInstance.transform);

        GameState.Instance.PlayerReference = prefabInstance.GetComponent<Player>();

        IProduct newProduct = prefabInstance.GetComponent<IProduct>();
        newProduct.Initialize();

        return newProduct;
    }

    private void ShrinkPlayer(Transform playerTransform)
    {
        Vector3 newSize = QTools.newSizeExponential(playerTransform.localScale, sizeMultiplierOnIteration, GameState.Instance.IterationCount);
        playerTransform.localScale = newSize;
    }

}
