using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class Inventory : Singleton<Inventory>
{
    public int Strawberries, StrawberryPairs, CleaningSupplies;
    public bool BookOne, BookTwo, BookThree;

    public int BookOneChapter, BookTwoChapter, BookThreeChapter;

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
}
