using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Look : MonoBehaviour
{
    private Camera mainCamera;
    private Transform playerModel;
    private Vector2 _mousePos;

    private void Awake()
    {
        mainCamera = Camera.main;
        playerModel = GameObject.Find("Player/PlayerModel").transform;
    }

    public void MouseInput(InputAction.CallbackContext context)
    {
        _mousePos = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(playerModel.position, playerModel.forward, Color.green, 5f);
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(_mousePos);
        if (Physics.Raycast(ray, out hit, 100f, ~10))
        {
            playerModel.LookAt(new Vector3(hit.point.x,transform.position.y,hit.point.z));
        }
    }
}
