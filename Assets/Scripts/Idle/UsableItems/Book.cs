using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : UsableItem
{
    public AudioClip PageEffect;
    public TextBox Text;

    protected override void useItem ()
    {
        var chapter = Inventory.Instance.GetAndIncrementChapter(ID);
        var text = Instantiate(Text);
        text.transform.SetParent(transform.root, false);
        SFX.Play(PageEffect);
        text.ReadLine.AddListener(p => SFX.Play(PageEffect));
    }
}
