using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlDiagramHighlighter : MonoBehaviour
{
    public Color OnColor, OffColor;
    public Image Up, Left, Right;
    public TextMeshProUGUI UpText, LeftText, RightText;

    void Update ()
    {
        Up.color = Input.GetButton("Flap") ? OnColor : OffColor;
        UpText.color = Up.color;

        bool turning = Input.GetButton("Horizontal");
        float axis = Input.GetAxisRaw("Horizontal");

        Left.color = turning && axis != 1 ? OnColor : OffColor;
        LeftText.color = Left.color;

        Right.color = turning && axis != -1 ? OnColor : OffColor;
        RightText.color = Right.color;
    }
}
