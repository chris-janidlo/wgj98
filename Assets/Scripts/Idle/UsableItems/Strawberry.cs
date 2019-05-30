using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strawberry : UsableItem
{
    public List<AudioClip> HappySounds;
    public float HungerAmount;

	protected override void useItem ()
	{
        DragonStats.Instance.Hunger.Value += HungerAmount;
        DragonEmoting.Instance.Animator.Play("DragonIdle_eating");
        SFX.Play(HappySounds);
	}
}
