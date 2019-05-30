using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class PauseMenu : Singleton<PauseMenu>
{
    bool pauseState;
    float scaleMem;

    void Awake ()
    {
        if (SingletonGetInstance() != null)
        {
            Destroy(gameObject);
        }
        else
        {
            SingletonSetInstance(this, false);
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetButtonUp("Cancel"))
        {
            SetPauseState(!pauseState);
        }    
    }
    
    public void SetPauseState (bool value)
    {
        if (pauseState == value) return;

        pauseState = value;

        transform.GetChild(0).gameObject.SetActive(value);

        if (value) scaleMem = Time.timeScale;

        Time.timeScale = value ? 0 : scaleMem;
    }

    public void QuitGame ()
    {
        Application.Quit();
    }
}
