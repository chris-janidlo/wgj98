using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using crass;

public class Tooltip : Singleton<Tooltip>
{
    public string Text
    {
        get => text.text;
        set { text.text = value; }
    }

    [SerializeField]
    TextMeshProUGUI text;

    void Awake ()
    {
        SingletonSetInstance(this, true);
    }

    void Update ()
    {
        transform.position = Input.mousePosition;
    }
}
