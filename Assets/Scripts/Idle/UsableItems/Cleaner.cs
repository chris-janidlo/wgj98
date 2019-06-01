using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : UsableItem
{
	public TextBox Text;
    public AudioClip BubbleSound;
    public float CleansingAmount;

	protected override void useItem ()
	{
		var text = Instantiate(Text);
		text.transform.SetParent(transform.root, false);

		text.Finished.AddListener(() => {
			DragonStats.Instance.Cleanliness.Value += CleansingAmount;
			SFX.Play(BubbleSound);
            Destroy(gameObject);
		});
	}
}
