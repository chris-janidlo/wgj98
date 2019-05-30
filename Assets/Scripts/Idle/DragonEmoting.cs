using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using crass;

public class DragonEmoting : Singleton<DragonEmoting>, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public float PetDelay;
    public float PetLoveIncrease;
    public int PetMoneyIncrease;
    public Animator Animator;

    public bool Hovered { get; private set; }
    
    float petTimer;

    void Awake ()
    {
        SingletonSetInstance(this, true);
    }

	void Update ()
    {
        Animator.SetFloat("Happiness", DragonStats.Instance.Happiness);
        petTimer -= Time.deltaTime;
    }

	public void OnPointerEnter(PointerEventData eventData)
	{
        Hovered = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
        Hovered = false;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
        if (petTimer <= 0)
        {
            petTimer = PetDelay;
            Animator.Play("DragonIdle_happy");
            DragonStats.Instance.Love.Value += PetLoveIncrease;
            Bank.Instance.IncrementMoney(PetMoneyIncrease);
        }
	}
}
