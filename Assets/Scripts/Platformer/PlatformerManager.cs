using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class PlatformerManager : MonoBehaviour
{
    [Serializable]
    public class GemPatternGroup
    {
        public List<GemPattern> Patterns;
    }

    public List<GemPatternGroup> Levels;

    [SerializeField]
    int level, subLevel;

    List<GemPattern> currentPatterns => Levels[level].Patterns;

    void Start ()
    {
        currentPatterns.ShuffleInPlace();
        spawnNextPattern();
        generateScreenEdgeColliders();
    }

    void spawnNextPattern ()
    {
        GemPattern next = Instantiate(currentPatterns[subLevel]);
        next.AllGemsCollected.AddListener(spawnNextPattern);

        subLevel++;
        if (subLevel == currentPatterns.Count)
        {
            level++;
            subLevel = 0;
            currentPatterns.ShuffleInPlace();
        }
    }

    // from https://forum.unity.com/threads/collision-with-sides-of-screen.228865/#post-4290190
    void generateScreenEdgeColliders ()
    {
        var camera = CameraCache.Main;

        Vector2 lDCorner = camera.ViewportToWorldPoint(new Vector3(0, 0f, camera.nearClipPlane));
        Vector2 rUCorner = camera.ViewportToWorldPoint(new Vector3(1f, 1f, camera.nearClipPlane));
        Vector2[] colliderpoints;
 
        EdgeCollider2D upperEdge = new GameObject("upperEdge").AddComponent<EdgeCollider2D>();
        upperEdge.transform.parent = transform;
        colliderpoints = upperEdge.points;
        colliderpoints[0] = new Vector2(lDCorner.x, rUCorner.y);
        colliderpoints[1] = new Vector2(rUCorner.x, rUCorner.y);
        upperEdge.points = colliderpoints;
 
        EdgeCollider2D lowerEdge = new GameObject("lowerEdge").AddComponent<EdgeCollider2D>();
        lowerEdge.transform.parent = transform;
        colliderpoints = lowerEdge.points;
        colliderpoints[0] = new Vector2(lDCorner.x, lDCorner.y);
        colliderpoints[1] = new Vector2(rUCorner.x, lDCorner.y);
        lowerEdge.points = colliderpoints;
 
        EdgeCollider2D leftEdge = new GameObject("leftEdge").AddComponent<EdgeCollider2D>();
        leftEdge.transform.parent = transform;
        colliderpoints = leftEdge.points;
        colliderpoints[0] = new Vector2(lDCorner.x, lDCorner.y);
        colliderpoints[1] = new Vector2(lDCorner.x, rUCorner.y);
        leftEdge.points = colliderpoints;
 
        EdgeCollider2D rightEdge = new GameObject("rightEdge").AddComponent<EdgeCollider2D>();
        rightEdge.transform.parent = transform;
        colliderpoints = rightEdge.points;
        colliderpoints[0] = new Vector2(rUCorner.x, rUCorner.y);
        colliderpoints[1] = new Vector2(rUCorner.x, lDCorner.y);
        rightEdge.points = colliderpoints;
    }
}
