using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private PlayerStats playerStats;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float jumpSpeed = 0.5f;
    [SerializeField] private float jumpControll = 0.5f;
    [SerializeField] private float dodgeSpeed = 4f;
    [SerializeField] private float dodgeDuration = 0.2f;
    [SerializeField] private float dodgeCooldown = 1f;

    private Transform groundCheck;
    private LayerMask mask;
    private Vector2 moveDir;
    private Vector2 lastMove;
    private bool isGrounded;
    private bool isJumping;
    private bool isDodging;
    private bool canDodge = true;
    private float groundDistance = 0.1f;

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
        mask = LayerMask.GetMask("Ground");
        groundCheck = PlayerManager.instance.player.transform.GetChild(1).transform;
        playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        movementSpeed = playerStats.GetMovementSpeed().GetValue();
    }

    private void Update()
    {
        if (!isGrounded)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, mask);
            if (isGrounded)
            {
                isJumping = false;
            }
        }
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, mask);
    }

    private void FixedUpdate()
    {
        if (!isJumping && !isDodging)
        {
            transform.position +=
                (Vector3.forward * moveDir.y + Vector3.right * moveDir.x) *
                Time.fixedDeltaTime * movementSpeed;
        }
        else if (isJumping)
        {
            transform.position +=
                ((Vector3.forward * lastMove.y + Vector3.right * lastMove.x) +
                (Vector3.forward * moveDir.y + Vector3.right * moveDir.x) * jumpControll) *
                Time.fixedDeltaTime * movementSpeed * jumpSpeed;
        }
        else if (isDodging)
        {
            transform.position +=
                (Vector3.forward * lastMove.y + Vector3.right * lastMove.x) *
                Time.fixedDeltaTime * movementSpeed * dodgeSpeed;
        }
    }

    public void Jump()
    {
        if (!isGrounded || isDodging)
            return;
        lastMove = moveDir;
        isJumping = true;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
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
