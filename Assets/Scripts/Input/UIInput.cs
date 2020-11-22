using UnityEngine;
using UnityEngine.InputSystem;

public class UIInput : MonoBehaviour
{
    private InputActionAsset inputActions;

    [SerializeField] GameObject inventoryUI;

    private void Awake()
    {
        inputActions = GetComponent<UnityEngine.InputSystem.PlayerInput>().actions;
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
