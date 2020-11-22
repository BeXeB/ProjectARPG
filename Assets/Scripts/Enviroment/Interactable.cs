using UnityEngine;

public class Interactable : MonoBehaviour
{
    public virtual void OnInteract()
    {
        Debug.Log(gameObject.name);
    }
}
