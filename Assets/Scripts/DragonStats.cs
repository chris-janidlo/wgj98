using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Assertions;
using crass;

public class DragonStats : Singleton<DragonStats>
{
    [Serializable]
    public class Meter
    {
        [SerializeField]
        float _value = 100;
        public float Value
        {
            get => _value;
            set
            {
                _value = Mathf.Clamp(value, 0, 100);
            }
        }

        public AnimationCurve DecayByValue;

        public float HappinessWeight;
        public float WeightedValue => Value * HappinessWeight;

        public void Update ()
        {
            Value -= DecayByValue.Evaluate(Value) * Time.deltaTime;
        }
    }

    public Meter Cleanliness, Hunger, Love;

    public float Happiness
    {
        get
        {
            Assert.AreEqual(Cleanliness.HappinessWeight + Hunger.HappinessWeight + Love.HappinessWeight, 1);

            return Cleanliness.WeightedValue + Hunger.WeightedValue + Love.WeightedValue;
        }
    }

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
        Cleanliness.Update();
        Hunger.Update();
        Love.Update();
    }
}
