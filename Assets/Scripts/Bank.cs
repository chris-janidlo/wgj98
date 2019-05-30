using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using crass;

public class Bank : Singleton<Bank>
{
    public AnimationCurve MoneyGainByHappiness;

    public float SecondsPerTick = 1;

    [SerializeField]
    float _money = 1;
    public int Money => (int) _money;

    float ticker;
    
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

    void Update ()
    {
        ticker += Time.deltaTime;
        if (ticker >= SecondsPerTick)
        {
            ticker = 0;
            CompoundMoney(MoneyGainByHappiness.Evaluate(DragonStats.Instance.Happiness));
        }

    }

    public void SetMoney (float value)
    {
        Assert.IsTrue(value >= 0, "can't have negative money");
        _money = value;
    }

    public void IncrementMoney (float delta)
    {
        SetMoney(_money + delta);
    }

    public void CompoundMoney (float percent)
    {
        IncrementMoney(Mathf.Max(_money, 1) * percent);
    }
}
