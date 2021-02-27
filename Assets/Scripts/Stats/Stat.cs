using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    public delegate void OnStatChange(Stat stat);
    public OnStatChange onStatChangeCallback;

    [SerializeField] private float baseValue;

    private List<float> modifiers = new List<float>();
    private List<float> percentageModifiers = new List<float>();

    public void SetBaseValue(float value)
    {
        baseValue = value;
        if (onStatChangeCallback != null)
        {
            onStatChangeCallback.Invoke(this);
        }
    }

    public float GetBaseValue()
    {
        return baseValue;
    }

    public float GetValue()
    {
        float finalValue = baseValue;
        modifiers.ForEach(x => finalValue += x);
        percentageModifiers.ForEach(x => finalValue *= 1f + (x / 100f));
        return finalValue;
    }

    public void AddModifier(float modifier)
    {
        if (modifier != 0)
        {
            modifiers.Add(modifier);
            if (onStatChangeCallback != null)
            {
                onStatChangeCallback.Invoke(this);
            }
        }
    }

    public void RemoveModifier(float modifier)
    {
        if (modifier != 0)
        {
            modifiers.Remove(modifier);
            if (onStatChangeCallback != null)
            {
                onStatChangeCallback.Invoke(this);
            }
        }
    }

    public void AddPercentageModifier(float percentageModifier)
    {
        if (percentageModifier != 0)
        {
            percentageModifiers.Add(percentageModifier);
            if (onStatChangeCallback != null)
            {
                onStatChangeCallback.Invoke(this);
            }
        }
    }

    public void RemovePercentageModifier(float percentageModifier)
    {
        if (percentageModifier != 0)
        {
            percentageModifiers.Remove(percentageModifier);
            if (onStatChangeCallback != null)
            {
                onStatChangeCallback.Invoke(this);
            }
        }
    }
}
