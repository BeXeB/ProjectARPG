using UnityEngine;
using UnityEngine.InputSystem;


public class CameraInput : MonoBehaviour
{
    private InputActionAsset inputActions;
    private Zoom cameraZoomScript;

    private void Awake()
    {
        inputActions = GetComponent<UnityEngine.InputSystem.PlayerInput>().actions;
        cameraZoomScript = gameObject.GetComponent<Zoom>();
    }

    public void ScrollInput(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        if (input.y != 0)
        {
            cameraZoomScript.setScroll(input);
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
