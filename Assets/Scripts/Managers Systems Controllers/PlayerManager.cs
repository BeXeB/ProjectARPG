using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    private void Awake() {
        if (instance)
        {
            Destroy(gameObject);
            return;
        } 
        instance = this;
    }

    public GameObject player;

}
