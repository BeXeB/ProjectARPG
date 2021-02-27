using System.Collections.Generic;
using UnityEngine;

public class SkillTreeStackController : MonoBehaviour
{
    public delegate void OnStacksChanged(int stacks);
    public OnStacksChanged onStacksChangedCallback;
    [SerializeField] Stat stackDuration;
    List<Stack> stacks = new List<Stack>();
    [SerializeField] private Stat maxStacks;

    public Stat GetMaxStacks()
    {
        return maxStacks;
    }

    public Stat GetStackDuration()
    {
        return stackDuration;
    }

    private void Start()
    {
        maxStacks.onStatChangeCallback += OnMaxStacksChanged;
    }

    private void OnMaxStacksChanged(Stat stat)
    {
        onStacksChangedCallback?.Invoke(stacks.Count);
    }

    private void Update()
    {
        stacks.ForEach(x =>
        {
            x.duration -= Time.deltaTime;
            if (x.duration <= 0f)
            {
                stacks.Remove(x);
                onStacksChangedCallback?.Invoke(stacks.Count);
            }
        });
    }

    public void AddStack()
    {
        if (stacks.Count < maxStacks.GetValue())
        {
            stacks.Add(new Stack { duration = stackDuration.GetValue() });
            onStacksChangedCallback.Invoke(stacks.Count);
        }
    }
}

[System.Serializable]
public struct Stack
{
    public float duration;
}