using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using crass;

public class DragonEmoting : Singleton<DragonEmoting>, IPointerEnterHandler, IPointerExitHandler
{
    public Animator Animator;

    public bool Hovered { get; private set; }

    void Awake ()
    {
        SingletonSetInstance(this, true);
    }

	void Update ()
    {
        Animator.SetFloat("Happiness", DragonStats.Instance.Happiness);
    }

	public void OnPointerEnter(PointerEventData eventData)
	{
        Hovered = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
        Hovered = false;
	}
}
