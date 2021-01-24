using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    public delegate void OnStatChange();
    public OnStatChange onStatChangeCallback;

    [SerializeField] private float baseValue;

    private List<float> modifiers = new List<float>();

    public void SetBaseValue(float value)
    {
        baseValue = value;
        if (onStatChangeCallback != null)
        {
            onStatChangeCallback.Invoke();
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
        return finalValue;
    }

    public void AddModifier(float modifier)
    {
        if (modifier != 0)
        {
            modifiers.Add(modifier);
            if (onStatChangeCallback != null)
            {
                onStatChangeCallback.Invoke();
            }
        }
    }

    public void RemoveModifier(float modifier)
    {
        if (modifier != 0)
        {
            modifiers.Remove(modifier);
        }
        if (onStatChangeCallback != null)
        {
            onStatChangeCallback.Invoke();
        }
    }
}
