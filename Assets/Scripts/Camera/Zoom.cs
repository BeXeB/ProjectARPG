using UnityEngine;

public class Zoom : MonoBehaviour
{
    [SerializeField] private float zoomSpeed = 50f;
    private Vector3 offset = new Vector3(0f, 10f, -10f);
    private Transform cameraTransform;
    private Transform playerTransform;
    private Vector2 scroll;

    public void setScroll(Vector2 newValue)
    {
        scroll = newValue;
    }

    private void Start()
    {
        cameraTransform = FindObjectOfType<Camera>().transform;
        playerTransform = PlayerManager.instance.player.transform;
    }

    private void Update()
    {
        if (scroll.y != 0)
        {
            ChangeOffset();
            offset.y = Mathf.Clamp(offset.y, 2f, 15f);
            offset.z = Mathf.Clamp(offset.z, -15f, -6f);
        }
        cameraTransform.parent.position = playerTransform.position + offset;
        cameraTransform.rotation = Quaternion.LookRotation(playerTransform.position - cameraTransform.parent.position);
    }

    private void ChangeOffset()
    {
        if (scroll.y > 0)
        {
            offset.y -= zoomSpeed * Time.deltaTime;
            offset.z += zoomSpeed * Time.deltaTime;
        }
        else
        {
            offset.y += zoomSpeed * Time.deltaTime;
            if (offset.y > 6f)
            {
                offset.z -= zoomSpeed * Time.deltaTime;
            }
        }
        scroll = Vector2.zero;
    }
}
