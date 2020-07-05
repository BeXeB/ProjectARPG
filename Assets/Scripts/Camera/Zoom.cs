using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Zoom : MonoBehaviour
{
    [SerializeField] private float zoomSpeed = 50f;
    private InputActionAsset inputActions;
    private Vector3 offset = new Vector3(0f, 10f, -10f);
    private Transform cameraTransform;
    private Transform playerTransform;
    private Vector2 scroll;

    public void ScrollInput(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        if (input.y != 0)
        {
            scroll = input;
        }
    }

    private void Awake()
    {
        inputActions = GetComponent<UnityEngine.InputSystem.PlayerInput>().actions;
        cameraTransform = GameObject.Find("Player/CameraHolder/MainCamera").transform;
        playerTransform = GameObject.Find("Player/PlayerModel").transform;
    }

    private void FixedUpdate()
    {
        if (scroll.y != 0)
        {
            ChangeOffset();
            offset.y = Mathf.Clamp(offset.y, 2f, 15f);
            offset.z = Mathf.Clamp(offset.z, -15f, -6f);
            cameraTransform.parent.localPosition = playerTransform.localPosition + offset;
        }
        cameraTransform.localRotation = Quaternion.LookRotation(playerTransform.localPosition - cameraTransform.parent.localPosition);
    }

    private void ChangeOffset()
    {
        if (scroll.y > 0)
        {
            offset.y -= zoomSpeed * Time.fixedDeltaTime;
            offset.z += zoomSpeed * Time.fixedDeltaTime;
        }
        else
        {
            offset.y += zoomSpeed * Time.fixedDeltaTime;
            if (offset.y > 6f)
            {
                offset.z -= zoomSpeed * Time.fixedDeltaTime;
            }
        }
        scroll = Vector2.zero;
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
