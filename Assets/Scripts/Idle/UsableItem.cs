using System.Collections;
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
    }

    void Update ()
    {
        transform.position = Input.mousePosition;

        if (Input.GetMouseButton(0) && DragonEmoting.Instance.Hovered)
        {
            useItem();
            Destroy(gameObject);
        }

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
