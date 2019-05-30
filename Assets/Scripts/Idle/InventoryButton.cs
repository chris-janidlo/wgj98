using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryButton : MonoBehaviour
{
    public UsableItem Item;
    public Button Button;
    public TextMeshProUGUI Text;

    int amount => Inventory.Instance.GetAmount(Item.ID);

    void Start ()
    {
        Button.onClick.AddListener(spawnItem);
    }

    void Update ()
    {
        Button.interactable = amount > 0 && !UsableItem.Locked;
        if ((int) Item.ID < 3)
        {
            Text.text = amount > 0 ? amount.ToString() : "";
        }
    }

    void spawnItem ()
    {
        if ((int) Item.ID < 3)
        {
            Inventory.Instance.SetAmount(Item.ID, amount - 1);
        }
        UsableItem item = Instantiate(Item);
        item.Canceled.AddListener(despawnItem);
        item.transform.parent = transform.root;

        GetComponentInParent<MenuPanel>().SetOpenState(false);
    }

    void despawnItem ()
    {
        if ((int) Item.ID < 3)
        {
            Inventory.Instance.SetAmount(Item.ID, amount + 1);
        }
    }
}
