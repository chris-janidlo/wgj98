using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : UsableItem
{
    public AudioClip PageEffect;
    public TextBox Text;

    protected override void useItem ()
    {
        var text = Instantiate(Text);
        text.transform.SetParent(transform.root, false);
        SFX.Play(PageEffect);
        text.ReadLine.AddListener(p => SFX.Play(PageEffect));
        switch (ID)
        {
            case InventoryID.BookOne:
                DragonStats.Instance.BookOneUnlocked = true;
                break;
                
            case InventoryID.BookTwo:
                DragonStats.Instance.BookTwoUnlocked = true;
                break;

            case InventoryID.BookThree:
                DragonStats.Instance.BookThreeUnlocked = true;
                break;
        }
    }
}
