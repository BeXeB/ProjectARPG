using UnityEngine;
using UnityEngine.InputSystem;

public class Look : MonoBehaviour
{
    [SerializeField] LayerMask mask;
    private Camera playerCamera;
    private Transform playerModel;
    private Vector2 _mousePos;

    public void setMousePos(Vector2 newValue){
        _mousePos = newValue;
    }

    private void Awake()
    {
        playerCamera = GameObject.Find("Player/CameraHolder/MainCamera").GetComponent<Camera>();
        playerModel = GameObject.Find("Player/PlayerModel").transform;
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(playerModel.position, playerModel.forward, Color.green, 5f);
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(_mousePos);
        if (Physics.Raycast(ray, out hit, 100f, mask))
        {
            playerModel.LookAt(new Vector3(hit.point.x,transform.position.y,hit.point.z));
        }
    }
}
