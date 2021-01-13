using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    public delegate void OnStatChange();
    public OnStatChange onStatChangeCallback;

    [SerializeField]
    private int baseValue;

    private List<int> modifiers = new List<int>();

    public void SetBaseValue(int value)
    {
        baseValue = value;
        if (onStatChangeCallback != null)
        {
            onStatChangeCallback.Invoke();
        }
    }

    public int GetBaseValue()
    {
        return baseValue;
    }

    public int GetValue()
    {
        int finalValue = baseValue;
        modifiers.ForEach(x => finalValue += x);
        return finalValue;
    }

    public void AddModifier(int modifier)
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

    public void RemoveModifier(int modifier)
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
