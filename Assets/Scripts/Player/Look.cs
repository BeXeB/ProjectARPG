using UnityEngine;

public class Look : MonoBehaviour
{
    [SerializeField] LayerMask mask;
    private Camera playerCamera;
    private Transform playerModel;
    private Vector2 _mousePos;

    public void setMousePos(Vector2 newValue)
    {
        _mousePos = newValue;
    }

    private void Awake()
    {
        playerCamera = PlayerManager.instance.player.transform.GetChild(2).GetChild(0).GetComponent<Camera>();
        playerModel = PlayerManager.instance.player.transform.GetChild(0);
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(playerModel.position, playerModel.forward, Color.green, 5f);
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(_mousePos);
        if (Physics.Raycast(ray, out hit, 100f, mask))
        {
            playerModel.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
        }
    }
}
