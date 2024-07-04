using UnityEngine;
using UnityEngine.EventSystems;

public class ShopItem : Item, IPointerEnterHandler, IPointerExitHandler
{
    public override void Initialize(ChairStruct chair) => base.Initialize(chair);

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        TooltipManager.Instance.ShowTooltip(false);
        temp.GetComponent<RectTransform>().sizeDelta = new Vector2(180f, 200f);
    }

    public override void OnDrag(PointerEventData eventData) => base.OnDrag(eventData);

    public override void OnEndDrag(PointerEventData eventData) => base.OnEndDrag(eventData);

    public void OnPointerEnter(PointerEventData eventData) => TooltipManager.Instance.ShowTooltip(itemName.text, price.ToString(), description);

    public void OnPointerExit(PointerEventData eventData) => TooltipManager.Instance.ShowTooltip(false);
}