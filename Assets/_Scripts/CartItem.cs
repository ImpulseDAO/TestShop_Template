using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CartItem : Item
{
    [SerializeField] private TextMeshProUGUI amountTMP;
    [SerializeField] private TextMeshProUGUI totalSumTMP;

    private int amount;
    private int totalSum;

    public int Amount { get { return amount; } set { if (amount + value >= 0) amount = value; } }

    public override void Initialize(ChairStruct chair)
    {
        base.Initialize(chair);
        UpdateAmount(1);
    }

    public void UpdateAmount(int val)
    {
        Amount += val;
        totalSum = Amount * chair.price;

        amountTMP.text = "x" + Amount;
        totalSumTMP.text = "$" + totalSum;
    }

    public override void OnBeginDrag(PointerEventData eventData) => base.OnBeginDrag(eventData);

    public override void OnDrag(PointerEventData eventData) => base.OnDrag(eventData);

    public override void OnEndDrag(PointerEventData eventData) => base.OnEndDrag(eventData);

    private void OnDestroy() => Destroy(temp);
}
