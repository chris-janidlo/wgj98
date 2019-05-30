using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public float CooldownTime;
    public Button Button;
    public Image CooldownDisplay;

    [SerializeField]
    float timer;

    void Start ()
    {
        timer = CooldownTime;
        Button.interactable = false;
        Button.onClick.AddListener(() => SceneManager.LoadScene("Platformer"));
    }

    void Update ()
    {
        CooldownDisplay.fillAmount = Mathf.Clamp(timer / CooldownTime, 0, 1);
        
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Button.interactable = true;
        }
    }
}
