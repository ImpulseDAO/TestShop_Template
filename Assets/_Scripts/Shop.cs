using UnityEngine;
using UnityEngine.EventSystems;

public class Shop : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData != null)
        {
            if (eventData.pointerDrag.TryGetComponent(out CartItem item))
            {
                ChairStorage.Instance.UpdateCartStorage(item.Chair, -1);
            }
        }
    }
}