using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{

    Movement movement;
    Transform playerModel;
    Animator animator;
    void Start()
    {
        playerModel = PlayerManager.instance.player.transform.GetChild(0);
        movement = GetComponent<Movement>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Vector3 movementV3 = new Vector3(movement.getMoveDir().x, 0f, movement.getMoveDir().y);
        float velocityZ = Vector3.Dot(movementV3.normalized, playerModel.forward);
        float velocityX = Vector3.Dot(movementV3.normalized, playerModel.right);
        animator.SetFloat("VelocityZ", velocityZ, 0.1f, Time.deltaTime);
        animator.SetFloat("VelocityX", velocityX, 0.1f, Time.deltaTime);
    }
}
