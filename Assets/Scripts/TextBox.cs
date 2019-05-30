using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class TextBox : MonoBehaviour, IPointerClickHandler
{
    [System.Serializable]
    public class IntEvent : UnityEvent<int> {}

    public IntEvent ReadLine;
    public UnityEvent Finished;

    [TextArea]
    public List<string> TargetText;

    public TextMeshProUGUI Box;

    int textIndex;

    void Start ()
    {
        Box.text = TargetText[0];
    }

	public void OnPointerClick (PointerEventData eventData)
	{
        ReadLine.Invoke(textIndex);
        textIndex++;
        if (textIndex == TargetText.Count)
        {
            Destroy(gameObject);
            Finished.Invoke();
        }
        else
        {
            Box.text = TargetText[textIndex];
        }
	}
}
