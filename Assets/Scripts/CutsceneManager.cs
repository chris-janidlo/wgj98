using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public Animator DragonAnimator;
    public GameObject Namer;
    public DragonStats StatsPrefab;

    public AudioSource DragonSource;
    public AudioClip LouderSad, Happy1, Happy2;

    public void TextRead (int text)
    {
        switch (text)
        {
            case 0:
                DragonSource.clip = LouderSad;
                DragonSource.Play(0);
                break;
            case 2:
                DragonSource.clip = Happy1;
                DragonSource.Play(0);
                DragonAnimator.SetFloat("Happiness", 100);
                break;
            case 3:
                DragonSource.clip = Happy2;
                DragonSource.Play(0);
                Namer.SetActive(true);
                break;
        }
    }

    public void DragonNamed (string value)
    {
        var stats = Instantiate(StatsPrefab);
        stats.Name = value;
        SceneManager.LoadScene("Idle");
    }
}
