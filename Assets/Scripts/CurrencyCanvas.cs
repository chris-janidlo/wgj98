using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using crass;

public class CurrencyCanvas : Singleton<CurrencyCanvas>
{
    public TextMeshProUGUI Text;

    void Awake ()
    {
        if (SingletonGetInstance() != null)
        {
            Destroy(gameObject);
        }
        else
        {
            SingletonSetInstance(this, false);
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update ()
    {
        Text.text = Bank.Instance.Money.ToString();
    }
}
