using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DragonName : MonoBehaviour
{
    void Update ()
    {
        GetComponent<TextMeshProUGUI>().text = DragonStats.Instance.Name;
    }
}
