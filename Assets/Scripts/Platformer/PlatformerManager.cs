using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using crass;

public class PlatformerManager : MonoBehaviour
{
    [Serializable]
    public class GemPatternGroup
    {
        public List<GemPattern> Patterns;
    }

    public float ModeTime, CooldownTime;
    public List<GemPatternGroup> Levels;

    public TextMeshProUGUI TimerText, CenterText;

    public int CountdownTime, FinishTime;
    [Range(0, 1)]
    public float FinishTimescale;

    [SerializeField]
    int level, subLevel;

    List<GemPattern> currentPatterns => Levels[level].Patterns;

    [SerializeField]
    float timer;
    bool ending;

    IEnumerator Start ()
    {
        generateScreenEdgeColliders();
        timer = ModeTime;
        Time.timeScale = 0;

        TimerText.gameObject.SetActive(false);
        CenterText.gameObject.SetActive(true);

        for (int i = CountdownTime; i >= 1; i--)
        {
            CenterText.text = i.ToString();
            yield return new WaitForSecondsRealtime(1);
        }

        Time.timeScale = 1;

        TimerText.gameObject.SetActive(true);
        currentPatterns.ShuffleInPlace();
        spawnNextPattern();

        CenterText.text = "GO";
        yield return new WaitForSecondsRealtime(1);
        CenterText.text = "";
    }

    void Update ()
    {
        TimerText.text = Mathf.CeilToInt(Mathf.Clamp(timer, 0, ModeTime)).ToString();

        timer -= Time.deltaTime;
        if (timer <= 0 && !ending)
        {
            ending = true;
            StartCoroutine(endRoutine());
        }
    }

    IEnumerator endRoutine ()
    {
        Time.timeScale = FinishTimescale;
        CenterText.text = "FINISH";
        
        yield return new WaitForSecondsRealtime(FinishTime);
        Time.timeScale = 1;
        DragonStats.Instance.PlayCooldown = CooldownTime;
        SceneManager.LoadScene("Idle");
    }

    void spawnNextPattern ()
    {
        GemPattern next = Instantiate(currentPatterns[subLevel]);
        next.AllGemsCollected.AddListener(spawnNextPattern);

        subLevel++;
        if (subLevel == currentPatterns.Count)
        {
            level = (level + 1) % Levels.Count;
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
