using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : UsableItem
{
    public AudioClip BubbleSound;
    public float CleansingAmount;

	protected override void useItem ()
	{
		DragonStats.Instance.Cleanliness.Value += CleansingAmount;
        SFX.Play(BubbleSound);
	}
}
