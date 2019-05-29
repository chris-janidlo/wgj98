using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GemPattern : MonoBehaviour
{
	public UnityEvent AllGemsCollected;
	public List<PlatformerGem> Gems;

	int gemCount, collectedCount;

	void Start ()
	{
		gemCount = Gems.Count;
		foreach (var gem in Gems)
		{
			gem.Collected.AddListener(gemCollected);
		}
	}

	void gemCollected ()
	{
		collectedCount++;
		if (collectedCount == gemCount)
		{
			Destroy(gameObject);
			AllGemsCollected.Invoke();
		}
	}
}
