using UnityEngine;

public class Zoom : MonoBehaviour
{
    [SerializeField] private float zoomSpeed = 50f;
    private Vector3 offset = new Vector3(0f, 10f, -10f);
    private Transform cameraTransform;
    private Transform playerTransform;
    private Vector2 scroll;

    public void setScroll(Vector2 newValue){
        scroll = newValue;
    }

    private void Awake()
    {
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
}
