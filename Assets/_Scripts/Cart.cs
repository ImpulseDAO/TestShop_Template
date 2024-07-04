using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cart : MonoBehaviour, IDropHandler
{
    public static Action<Checkout> CheckoutMade;

    [SerializeField] private TextMeshProUGUI totalCartSumTMP;

    private int totalCartSum;
    private int coef = 1; 

    public void DeleteAllItems()
    {
        UpdateTotalSum(0);
        foreach (var item in ChairStorage.Instance.CartChairs)
        {
            Destroy(item.Value);
        }
        ChairStorage.Instance.CartChairs.Clear();
    }

    public void Checkout()
    {
        Checkout response = new();
        response.checkoutChairs = new CheckoutChair[ChairStorage.Instance.CartChairs.Count];

        int i = 0;
        foreach (var item in ChairStorage.Instance.CartChairs)
        {
            item.Value.TryGetComponent(out CartItem ctItem);
            var temp = new CheckoutChair { id = ctItem.Chair.id, amount = ctItem.Amount };
            response.checkoutChairs.SetValue(temp, i);
            i++;
        }

        CheckoutMade?.Invoke(response);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData != null)
        {
            if (eventData.pointerDrag.TryGetComponent(out ShopItem item))
                ChairStorage.Instance.UpdateCartStorage(item.Chair);
        }
    }

    private void OnEnable()
    {
        ChairStorage.Instance.CartItemUpdated += (item, val) => UpdateCartItemAmount(item, val);
        ChairStorage.Instance.NewCartItemAdded += (item) => UpdateTotalSum(item, coef);

    }

    private void OnDisable()
    {
        ChairStorage.Instance.CartItemUpdated -= UpdateCartItemAmount;
        ChairStorage.Instance.NewCartItemAdded -= (item) => UpdateTotalSum(item, coef);
    }

    private void UpdateCartItemAmount(ChairStruct item, int val)
    {
        if (ChairStorage.Instance.CartChairs.TryGetValue(item.id, out GameObject ch))
        {
            if (ch.TryGetComponent(out CartItem ctItem))
            {
                if (ctItem.Amount + val <= 0)
                {
                    Destroy(ch);
                    ChairStorage.Instance.CartChairs.Remove(item.id);
                }
                coef = val < 0 ? -1 : 1;

                ctItem.UpdateAmount(val);
            }
            UpdateTotalSum(item, coef);
        }
        coef = 1;
    }

    private void UpdateTotalSum(ChairStruct item, int val)
    {
        totalCartSum += item.price * val;
        totalCartSumTMP.text = "$" + totalCartSum;
    }

    private void UpdateTotalSum(int val)
    {
        totalCartSum = val;
        totalCartSumTMP.text = "$" + totalCartSum;
    }
}
