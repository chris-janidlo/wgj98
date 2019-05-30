using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public Animator DragonAnimator;
    public GameObject Namer;
    public DragonStats StatsPrefab;

    public void TextRead (int text)
    {
        switch (text)
        {
            case 0:
                // play louder sad sound
                break;
            case 2:
                // play cute sound
                DragonAnimator.SetFloat("Happiness", 100);
                break;
            case 3:
                // play happy sound
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
