using System;
using System.Collections.Generic;
using UnityEngine;

public class ChairStorage : MonoBehaviour
{
    public Action UpdatedShopStorage;
    public Action<ChairStruct> NewCartItemAdded;
    public Action<ChairStruct, int> CartItemUpdated;

    private Dictionary<int, ChairStruct> shopChairs = new();
    private Dictionary<int, GameObject> cartChairsPrefabs = new();

    public static ChairStorage Instance { get; private set; }

    public Dictionary<int, ChairStruct> ShopChairs { get => shopChairs; private set => shopChairs = value; }
    public Dictionary<int, GameObject> CartChairs { get => cartChairsPrefabs; private set => cartChairsPrefabs = value; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    public void UpdateShopStorage(ChairsStructFromJson chairs)
    {
        foreach (var item in chairs.chairs)
        {
            this.ShopChairs.TryAdd(item.id, item);
        }

        UpdatedShopStorage?.Invoke();
    }

    public void UpdateCartStorage(ChairStruct item)
    {
        if (!CartChairs.ContainsKey(item.id))
        {
            NewCartItemAdded?.Invoke(item);
        }
        else
            CartItemUpdated?.Invoke(item, 1);
    }

    public void UpdateCartStorage(ChairStruct item, int val)
    {
        CartItemUpdated?.Invoke(item, val);
    }
}
