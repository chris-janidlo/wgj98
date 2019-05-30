using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using crass;

public class DragonEmoting : Singleton<DragonEmoting>, IPointerClickHandler
{
    public UnityEvent Clicked;
    public List<AudioClip> HappySounds;
    public float PetDelay;
    public float PetLoveIncrease;
    public int PetMoneyIncrease;
    public Animator Animator;
    
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

	public void OnPointerClick (PointerEventData eventData)
	{
        if (petTimer <= 0 && !UsableItem.Locked)
        {
            petTimer = PetDelay;
            Animator.Play("DragonIdle_happy");
            DragonStats.Instance.Love.Value += PetLoveIncrease;
            Bank.Instance.IncrementMoney(PetMoneyIncrease);
            SFX.Play(HappySounds);
        }

        Clicked.Invoke();
	}
}
