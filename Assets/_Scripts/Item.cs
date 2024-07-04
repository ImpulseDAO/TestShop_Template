using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] protected Image sprite;
    [SerializeField] protected TextMeshProUGUI itemName;

    protected ChairStruct chair;
    protected string spritePath = "Assets/Images/";
    protected string spriteName;
    protected string description;
    protected int price;
    protected GameObject temp;

    public ChairStruct Chair { get => chair; private set { chair = value; } }

    public virtual void Initialize(ChairStruct chair)
    {
        this.chair = chair;
        this.spriteName = chair.filename;
        spritePath += spriteName;
        Sprite sprite = (Sprite)AssetDatabase.LoadAssetAtPath(spritePath, typeof(Sprite));
        this.sprite.sprite = sprite;

        this.itemName.text = chair.name;
        this.description = chair.description;
        this.price = chair.price;
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        temp = Instantiate(gameObject, GameManager.Instance.dragLayer);
        if (temp.gameObject.TryGetComponent(out Graphic gr))
            gr.raycastTarget = false;
        for(int i = 1; i < temp.transform.childCount; i++)
            temp.transform.GetChild(i).gameObject.SetActive(false);
    }

    public virtual void OnDrag(PointerEventData eventData) => temp.transform.position = eventData.position;

    public virtual void OnEndDrag(PointerEventData eventData) => Destroy(temp); 
}