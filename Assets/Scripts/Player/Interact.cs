using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] float interactRange = 5f;
    [SerializeField] LayerMask mask;
    private Camera playerCamera;
    private Vector2 _mousePos;

    public void setMousePos(Vector2 newValue)
    {
        _mousePos = newValue;
    }

    private void Start()
    {
        playerCamera = FindObjectOfType<Camera>();
    }

    public void TryInteract()
    {
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(_mousePos);
        if (Physics.Raycast(ray, out hit, 100f, mask))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null && Vector3.Distance(transform.position, hit.collider.transform.position) <= interactRange)
            {
                interactable.OnInteract();
            }
        }
    }
}
