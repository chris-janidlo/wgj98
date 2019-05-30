using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopButton : MonoBehaviour
{
    public Button Button;
    public TextMeshProUGUI Text;
    
    int _price;
    public int Price
    {
        get => _price;
        set
        {
            _price = value;
            Text.text = $"x {_price}";
        }
    }

    void Start ()
    {
        Button.onClick.AddListener(() => Bank.Instance.IncrementMoney(-Price));
    }

    void Update ()
    {
        Button.interactable = Bank.Instance.Money >= Price;
    }
}
