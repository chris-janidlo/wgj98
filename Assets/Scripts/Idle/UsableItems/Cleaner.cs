using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : UsableItem
{
    public float CleansingAmount;

	protected override void useItem ()
	{
		DragonStats.Instance.Cleanliness.Value += CleansingAmount;
        // TODO: animate bubbles
	}
}
