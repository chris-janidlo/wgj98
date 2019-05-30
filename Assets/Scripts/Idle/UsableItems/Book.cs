using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : UsableItem
{
    protected override void useItem ()
    {
        var chapter = Inventory.Instance.GetAndIncrementChapter(ID);
        // TODO: read text
    }
}
