using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : UsableItem
{
    public TextBox Text;

    protected override void useItem ()
    {
        var chapter = Inventory.Instance.GetAndIncrementChapter(ID);
        Instantiate(Text).transform.SetParent(transform.root, false);
    }
}
