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

    [SerializeField] private Transform playerSpawnAnchor;
    [SerializeField] private Camera playerCamera;

    [Tooltip("1,1,1 means nothing happens, between 0 and 1 shrinks, above 1 grows.")]
    [SerializeField] private Vector3 sizeMultiplierOnIteration = new(1f, 1f, 1f);

    [Header("Readonly")]
    [SerializeField] private int currentIteration;

    private void Awake()
    {
        SingletonSetup();
    }

    public override IProduct SpawnProduct(Vector3 position)
    {
        GameObject prefabInstance = Instantiate(prefab,
        position, Quaternion.identity);

        currentIteration++;
        ShrinkPlayer(prefabInstance.transform);

        Player playerScript = prefabInstance.GetComponent<Player>();
        playerScript.spawnAnchor = playerSpawnAnchor;
        playerScript.GetComponent<PlayerMovement>().playerCamera = playerCamera;

        IProduct newProduct = prefabInstance.GetComponent<IProduct>();
        newProduct.Initialize();

        return newProduct;
    }

    private void ShrinkPlayer(Transform playerTransform)
    {
        Vector3 newSize = QTools.newSizeExponential(playerTransform.localScale, sizeMultiplierOnIteration, currentIteration);
        playerTransform.localScale = newSize;
    }

}
