using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private PlayerStats playerStats;
    private CharacterController controller;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float jumpSpeed = 0.5f;
    [SerializeField] private float jumpControll = 0.5f;
    [SerializeField] private float dodgeSpeed = 4f;
    [SerializeField] private float dodgeDuration = 0.2f;
    [SerializeField] private float dodgeCooldown = 1f;
    [SerializeField] private float fallSpeed = 1.5f;
    private Transform groundCheck;
    private LayerMask mask;
    private Vector2 moveDir;
    private Vector2 lastMove;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isJumping;
    private bool isDodging;
    private bool canDodge = true;
    private float groundDistance = 0.1f;
    private float gravity = 9.8f;

    #region Getters/Setters

    public bool getIsGrounded()
    {
        return isGrounded;
    }

    public bool getIsDodgeing()
    {
        return isDodging;
    }

    public bool getCanDodge()
    {
        return canDodge;
    }

    public Vector2 getMoveDir()
    {
        return moveDir;
    }

    public void setMoveDir(Vector2 newValue)
    {
        moveDir = newValue;
    }

    #endregion

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        mask = LayerMask.GetMask("Ground");
        groundCheck = PlayerManager.instance.player.transform.GetChild(1).transform;
        playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        movementSpeed = playerStats.GetMovementSpeed().GetValue();
    }

    private void Update()
    {
        if (!isGrounded)
        {
            isGrounded = Physics.CheckBox(groundCheck.position, new Vector3(0.25f, groundDistance, 0.25f), transform.rotation, mask);
            if (isGrounded)
            {
                isJumping = false;
            }
        }
        isGrounded = Physics.CheckBox(groundCheck.position, new Vector3(0.25f, groundDistance, 0.25f), transform.rotation, mask);
    }

    private void FixedUpdate()
    {
        Gravity();
        if (!isJumping && !isDodging)
        {
            controller.Move(
                (Vector3.forward * moveDir.y + Vector3.right * moveDir.x) *
                Time.fixedDeltaTime * movementSpeed);
        }
        else if (isJumping)
        {
            controller.Move(
                ((Vector3.forward * lastMove.y + Vector3.right * lastMove.x) +
                (Vector3.forward * moveDir.y + Vector3.right * moveDir.x) * jumpControll) *
                Time.fixedDeltaTime * movementSpeed * jumpSpeed);
        }
        else if (isDodging)
        {
            controller.Move(
                (Vector3.forward * lastMove.y + Vector3.right * lastMove.x) *
                Time.fixedDeltaTime * movementSpeed * dodgeSpeed);
        }
    }

    void Gravity()
    {
        if (!isGrounded)
        {
            velocity.y = Mathf.Clamp(velocity.y, -40f, 15f);
        }
        else
        {
            velocity.y = Mathf.Clamp(velocity.y, 0f, 15f);
        }
        velocity.y -= gravity * Time.fixedDeltaTime;
        controller.Move(velocity * Time.fixedDeltaTime * fallSpeed);
    }

    public void Jump()
    {
        if (!isGrounded || isDodging)
            return;
        lastMove = moveDir;
        isJumping = true;
        velocity.y = jumpForce;
    }

    public IEnumerator Dodge()
    {
        canDodge = false;
        lastMove = moveDir;
        isDodging = true;
        yield return new WaitForSeconds(dodgeDuration);
        isDodging = false;
        yield return new WaitForSeconds(dodgeCooldown);
        canDodge = true;
    }
}
