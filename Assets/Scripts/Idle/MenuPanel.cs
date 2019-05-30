using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuPanel : MonoBehaviour
{
    [Serializable]
    public class BoolEvent : UnityEvent<bool> {}

    public BoolEvent StateChanged;
    bool openState;

    void Update ()
    {
        if (Input.GetMouseButton(0) && !RectTransformUtility.RectangleContainsScreenPoint(GetComponent<RectTransform>(), Input.mousePosition))
        {
            SetOpenState(false);
        }
    }

    public void SetOpenState (bool value)
    {
        if (openState == value) return;

        openState = value;

        StateChanged.Invoke(value);

        transform.GetChild(0).gameObject.SetActive(value);
    }
}
