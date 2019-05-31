using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class Inventory : Singleton<Inventory>
{
    public int Strawberries, StrawberryPairs, CleaningSupplies;
    public bool BookOne, BookTwo, BookThree;

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

    public int GetAmount (InventoryID id)
    {
        switch (id)
        {
            case InventoryID.Strawberry:
                return Strawberries;

            case InventoryID.StrawberryPair:
                return StrawberryPairs;

            case InventoryID.Cleaner:
                return CleaningSupplies;

            case InventoryID.BookOne:
                return BookOne ? 1 : 0;

            case InventoryID.BookTwo:
                return BookTwo ? 1 : 0;

            case InventoryID.BookThree:
                return BookThree ? 1 : 0;

            default:
                throw new System.Exception($"unexpected id {id}");
        }
    }

    public void SetAmount (InventoryID id, int value)
    {
        switch (id)
        {
            case InventoryID.Strawberry:
                Strawberries = value;
                break;

            case InventoryID.StrawberryPair:
                StrawberryPairs = value;
                break;

            case InventoryID.Cleaner:
                CleaningSupplies = value;
                break;
                
            default:
                throw new System.Exception($"unexpected id {id}");
        }
    }
}

public enum InventoryID
{
    Strawberry, StrawberryPair, Cleaner, BookOne, BookTwo, BookThree
}
