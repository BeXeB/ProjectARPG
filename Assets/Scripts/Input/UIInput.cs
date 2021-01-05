using UnityEngine;
using UnityEngine.InputSystem;

public class UIInput : MonoBehaviour
{
    private InputActionAsset inputActions;

    [SerializeField] GameObject inventoryUI;
    [SerializeField] GameObject skillsUI;

    private void Awake()
    {
        inputActions = GetComponent<UnityEngine.InputSystem.PlayerInput>().actions;
    }

    public void SkillsInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            skillsUI.SetActive(!skillsUI.activeSelf);
        }
    }
    public void InventoryInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
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
