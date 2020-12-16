using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private Movement playerMovementScript;
    private Look playerLookScript;
    private Interact playerInteractScript;
    private PlayerStats playerStatsScript;

    private void Start()
    {
        playerMovementScript = GetComponent<Movement>();
        playerLookScript = GetComponent<Look>();
        playerInteractScript = GetComponent<Interact>();
        playerStatsScript = GetComponent<PlayerStats>();
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
            var weapon = (Weapon)Equipment.instance.GetEquipment()[8];
            if (weapon)
            {
                weapon.Attack(playerStatsScript.CalcWeaponDmg());
            }
        }
    }
}
