﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class UsableItem : MonoBehaviour
{
    public static bool Locked = false;

    public InventoryID ID;
    public UnityEvent Canceled;

    void Start ()
    {
        Locked = true;

        DragonEmoting.Instance.Clicked.AddListener(() => {
            useItem();
            Destroy(gameObject);
        });
    }

    void Update ()
    {
        transform.position = Input.mousePosition;

        if (Input.GetMouseButton(1))
        {
            Canceled.Invoke();
            Destroy(gameObject);
        }
    }

    void OnDestroy ()
    {
        Locked = false;
    }

    protected abstract void useItem ();
}
