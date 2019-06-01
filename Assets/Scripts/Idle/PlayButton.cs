using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public TextBox TextToSpawn;
    public float CooldownTime;
    public Button Button;
    public Image CooldownDisplay;

    void Start ()
    {
        Button.interactable = false;
        Button.onClick.AddListener(buttonClicked);
    }

    void Update ()
    {
        CooldownDisplay.fillAmount = Mathf.Clamp(DragonStats.Instance.PlayCooldown / CooldownTime, 0, 1);
        
        DragonStats.Instance.PlayCooldown -= Time.deltaTime;
        if (DragonStats.Instance.PlayCooldown <= 0)
        {
            Button.interactable = true;
        }
    }

    void buttonClicked ()
    {
        var text = Instantiate(TextToSpawn);
        text.transform.SetParent(transform.root, false);
        text.Finished.AddListener(() => SceneManager.LoadScene("Platformer"));
    }
}
