using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private InputActionAsset inputActions;
    private Movement playerMovementScript;
    private Look playerLookScript;
    private Interact playerInteractScript;
    private PlayerAttack playerAttackScript;

    private void Awake()
    {
        inputActions = GetComponent<UnityEngine.InputSystem.PlayerInput>().actions;
        playerMovementScript = GetComponent<Movement>();
        playerLookScript = GetComponent<Look>();
        playerInteractScript = GetComponent<Interact>();
        playerAttackScript = GetComponent<PlayerAttack>();
    }

    public void InteractInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            playerInteractScript.TryInteract();
        }
    }

    public void MouseInput(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        playerLookScript.setMousePos(value);
        playerInteractScript.setMousePos(value);
    }

    public void MoveInput(InputAction.CallbackContext context)
    {
        playerMovementScript.setMoveDir(context.ReadValue<Vector2>());
    }

    public void JumpInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            playerMovementScript.Jump();
        }
    }

    public void DodgeInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!playerMovementScript.getIsGrounded() || !playerMovementScript.getCanDodge())
                return;
            StartCoroutine(playerMovementScript.Dodge());
        }
    }

    public void AttackInput(InputAction.CallbackContext context)
    {
        if (context.performed && !playerMovementScript.getIsDodgeing())
        {
            playerAttackScript.Attack();
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
