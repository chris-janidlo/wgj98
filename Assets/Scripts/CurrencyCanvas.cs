using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using crass;

public class CurrencyCanvas : Singleton<CurrencyCanvas>, IPointerEnterHandler, IPointerExitHandler
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
            DontDestroyOnLoad(transform.root.gameObject);
        }
    }

    void Update ()
    {
        Text.text = Bank.Instance.Money.ToString();
    }

	public void OnPointerEnter (PointerEventData eventData)
	{
        // will get mad in platformer mode but ehh
        Tooltip.Instance.Text = "your gem count\n(your dragon has the\npower to change this, so\nmake sure they're happy!)";
	}

	public void OnPointerExit (PointerEventData eventData)
	{
        Tooltip.Instance.Text = "";
	}
}
