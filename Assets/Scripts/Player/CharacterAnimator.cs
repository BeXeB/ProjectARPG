using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{

    Movement movement;
    Transform playerGraphics;
    Animator animator;
    void Start()
    {
        playerGraphics = PlayerManager.instance.player.transform.GetChild(0);
        movement = GetComponent<Movement>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Vector3 movementV3 = new Vector3(movement.getMoveDir().x, 0f, movement.getMoveDir().y);
        float velocityZ = Vector3.Dot(movementV3.normalized, playerGraphics.forward);
        float velocityX = Vector3.Dot(movementV3.normalized, playerGraphics.right);

        LimitVelocity(ref velocityX);
        LimitVelocity(ref velocityZ);

        animator.SetFloat("VelocityZ", velocityZ, 0.1f, Time.deltaTime);
        animator.SetFloat("VelocityX", velocityX, 0.1f, Time.deltaTime);
    }

    private void LimitVelocity(ref float velocity)
    {
        if (velocity >= 0.80f)
        {
            velocity = 1;
        }
        else if (velocity >= 0.35f)
        {
            velocity = 0.7f;
        }
        else if (velocity >= -0.35f)
        {
            velocity = 0;
        }
        else if (velocity >= -0.80f)
        {
            velocity = -0.7f;
        }
        else
        {
            velocity = -1;
        }
    }
}
