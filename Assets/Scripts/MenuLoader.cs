using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLoader : MonoBehaviour
{
    public float Delay;

    IEnumerator Start ()
    {
        yield return new WaitForSeconds(Delay);
        while (true)
        {
            if (Input.anyKey)
            {
                SceneManager.LoadScene("IntroCutscene");
            }
            yield return null;
        }
    }
}
