using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformElement : MonoBehaviour
{
    const float spawnTime = .5f;

    IEnumerator Start ()
    {
        float origScale = ((Vector2) transform.localScale).magnitude;
        transform.localScale = Vector2.zero;

        float timer = spawnTime;
        while (timer >= 0)
        {
            float size = Mathf.Lerp(0, origScale, 1 - (timer / spawnTime));
            transform.localScale = new Vector3(size, size, 1);
            timer -= Time.deltaTime;
            yield return null;
        }

        transform.localScale = new Vector3(origScale, origScale, 1);
    }
}
