using UnityEngine;
using System;
using System.Collections.Generic;


[Serializable]
public class Stat
{
    [SerializeField] private float baseValue;
    [SerializeField] private List<StatModifier> modifiers = new List<StatModifier>();

    private bool wasModified;
    private float finalValue;

    public float GetValue()
    {
        if (wasModified)
        {
            finalValue = UpdateFinalValue();
            wasModified = false;
        }

        return finalValue;
    }

    public void AddModifier(float value, string source)
    {
        modifiers.Add(new StatModifier(value, source));
        wasModified = true;
    }

    public void RemoveModifier(string source)
    {
        modifiers.RemoveAll(modifier => modifier.source == source);
        wasModified = true;
    }

    private float UpdateFinalValue()
    {
        finalValue = baseValue;
        foreach (StatModifier modifier in modifiers)
        {
            finalValue += modifier.value;
        }
        return finalValue;
    }

    public void SetBaseValue(float value)
    {
        baseValue = value;
        wasModified = true;
    }

}
    [Serializable]
    public class StatModifier
    {
        public float value;
        public string source;

        public StatModifier(float value, string source)
        {
            this.value = value;
            this.source = source;
        }
    }
