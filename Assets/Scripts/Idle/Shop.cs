using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [Serializable]
    public class Inflation
    {
        [Serializable]
        public class InflationPoint
        {
            public int ActivatingWorth, Price;
        }

        public List<InflationPoint> InflationData;
        public int Counter;

        public int GetCurrentPrice ()
        {
            if (Counter <= InflationData.Count && Bank.Instance.Money >= InflationData[Counter].ActivatingWorth)
            {
                Counter++;
            }

            return InflationData[Counter].Price;
        }
    }

    public Inflation StrawberryInflation, StrawberryPairInflation, CleanerInflation;

    public int BookOnePrice, BookTwoPrice, BookThreePrice;

    public ShopButton Strawberry, StrawberryPair, Cleaner, BookOne, BookTwo, BookThree;

    bool openState;

    void Start ()
    {
        Strawberry.Button.onClick.AddListener(() => Inventory.Instance.Strawberries++);
        StrawberryPair.Button.onClick.AddListener(() => Inventory.Instance.StrawberryPairs++);
        Cleaner.Button.onClick.AddListener(() => Inventory.Instance.CleaningSupplies++);

        BookOne.Button.onClick.AddListener(() => { Inventory.Instance.BookOne = true; removeOwnedBooks(); });
        BookTwo.Button.onClick.AddListener(() => { Inventory.Instance.BookTwo = true; removeOwnedBooks(); });
        BookThree.Button.onClick.AddListener(() => { Inventory.Instance.BookThree = true; removeOwnedBooks(); });

        BookOne.Price = BookOnePrice;
        BookTwo.Price = BookTwoPrice;
        BookThree.Price = BookThreePrice;
        applyInflation();
    }
    
    public void SetOpenState (bool value)
    {
        if (value)
        {
            applyInflation();
            removeOwnedBooks();
        }
    }

    void applyInflation ()
    {
        Strawberry.Price = StrawberryInflation.GetCurrentPrice();
        StrawberryPair.Price = StrawberryPairInflation.GetCurrentPrice();
        Cleaner.Price = CleanerInflation.GetCurrentPrice();
    }

    void removeOwnedBooks ()
    {
        if (Inventory.Instance.BookOne)
        {
            BookOne.gameObject.SetActive(false);
        }

        if (Inventory.Instance.BookTwo)
        {
            BookTwo.gameObject.SetActive(false);
        }

        if (Inventory.Instance.BookThree)
        {
            BookThree.gameObject.SetActive(false);
        }
    }
}
