using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkillInput : MonoBehaviour
{
    private InputActionAsset inputActions;

    private SkillBar skillBar;
    private void Awake()
    {
        inputActions = GetComponent<UnityEngine.InputSystem.PlayerInput>().actions;
        skillBar = GetComponent<SkillBar>();
    }

    public void Skill1Input(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            skillBar.TryUseSkill(0);
        }
    }

    public void Skill2Input(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            skillBar.TryUseSkill(1);
        }
    }

    public void Skill3Input(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            skillBar.TryUseSkill(2);
        }
    }

    public void Skill4Input(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            skillBar.TryUseSkill(3);
        }
    }
    
    void OnEnable()
    {
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }
}
