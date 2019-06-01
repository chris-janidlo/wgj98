using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueToggle : MonoBehaviour
{
    public Toggle Toggle;

    void Start ()
    {
        Toggle.onValueChanged.AddListener(b => DragonStats.Instance.IsBlue = b);
    }

    void Update ()
    {
        Toggle.interactable = DragonStats.Instance.BookThreeUnlocked;
    }
}
