using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public RectTransform dragLayer;

    [SerializeField] private GameObject shopPrefab;
    [SerializeField] private GameObject cartPrefab;
    [SerializeField] private Transform shopParentTransform;
    [SerializeField] private Transform cartParentTransform;

    private void OnEnable()
    {
        ChairStorage.Instance.UpdatedShopStorage += CreateShopItems;
        ChairStorage.Instance.NewCartItemAdded += item => AddNewCartItem(item);
    }

    private void OnDisable()
    {
        ChairStorage.Instance.UpdatedShopStorage -= CreateShopItems;
        ChairStorage.Instance.NewCartItemAdded -= AddNewCartItem;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    private void CreateShopItems()
    {
        foreach (var item in ChairStorage.Instance.ShopChairs)
        {
            GameObject newItem = Instantiate(shopPrefab, shopParentTransform);
            var scr = newItem.GetComponent<ShopItem>();
            scr.Initialize(item.Value);
        }
    }

    private void AddNewCartItem(ChairStruct chair)
    {
        GameObject newItem = Instantiate(cartPrefab, cartParentTransform);
        var scr = newItem.GetComponent<CartItem>();
        scr.Initialize(chair);
        ChairStorage.Instance.CartChairs.Add(scr.Chair.id, newItem);
    }
}