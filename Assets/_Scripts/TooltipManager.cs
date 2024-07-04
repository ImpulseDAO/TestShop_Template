using TMPro;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI price;
    [SerializeField] private TextMeshProUGUI description;

    public static TooltipManager Instance { get; private set; }

    public void ShowTooltip(string name, string price, string description)
    {
        this.itemName.text = name.ToUpper();
        this.price.text = "$" + price;
        this.description.text = description;
        gameObject.SetActive(true);
        transform.position = Input.mousePosition;
    }

    public void ShowTooltip(bool isActive) => gameObject.SetActive(isActive);

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        gameObject.SetActive(false);
    }
}
