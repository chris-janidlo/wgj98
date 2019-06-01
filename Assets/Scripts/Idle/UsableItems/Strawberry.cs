using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strawberry : UsableItem
{
    public TextBox TextToSpawn;
    public List<AudioClip> HappySounds;
    public float HungerAmount;

	protected override void useItem ()
	{
        var text = Instantiate(TextToSpawn);
        text.transform.SetParent(transform.root, false);

        text.ReadLine.AddListener(l => {
            if (l == 1)
            {
                DragonStats.Instance.Hunger.Value += HungerAmount;
                DragonEmoting.Instance.Animator.Play("DragonIdle_eating");
                SFX.Play(HappySounds);
                Destroy(gameObject);
            }
        });
	}
}
