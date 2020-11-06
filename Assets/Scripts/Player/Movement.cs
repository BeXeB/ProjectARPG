using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float jumpSpeed = 0.5f;
    [SerializeField] private float jumpControll = 0.5f;
    [SerializeField] private float dodgeSpeed = 4f;
    [SerializeField] private float dodgeDuration = 0.2f;

    private Transform cameraHolder;
    private Transform groundCheck;
    private LayerMask mask;
    private Vector2 moveDir;
    private Vector2 lastMove;
    private bool isGrounded;
    private bool isJumping;
    private bool isDodging;
    private bool isSprinting;
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

    public bool getIsSprinting()
    {
        return isSprinting;
    }

    public void setMoveDir(Vector2 newValue)
    {
        moveDir = newValue;
    }

    #endregion

    private void Awake()
    {
        mask = LayerMask.GetMask("Ground");
        groundCheck = GameObject.Find("Player/GroundCheck").transform;
        cameraHolder = GameObject.Find("Player/CameraHolder").transform;
    }

    public void FixedUpdate()
    {
        if (!isJumping && !isDodging)
        {
            transform.position +=
                (cameraHolder.forward * moveDir.y + cameraHolder.right * moveDir.x) *
                Time.fixedDeltaTime * movementSpeed;
        }
        else if (isJumping)
        {
            transform.position +=
                ((cameraHolder.forward * lastMove.y + cameraHolder.right * lastMove.x) +
                (cameraHolder.forward * moveDir.y + cameraHolder.right * moveDir.x) * jumpControll) *
                Time.fixedDeltaTime * movementSpeed * jumpSpeed;
        }
        else if (isDodging)
        {
            transform.position +=
                (cameraHolder.forward * lastMove.y + cameraHolder.right * lastMove.x) *
                Time.fixedDeltaTime * movementSpeed * dodgeSpeed;
        }
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

    public void Jump()
    {
        lastMove = moveDir;
        isJumping = true;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    public void Sprint()
    {
        if (!isSprinting)
        {
            isSprinting = true;
            movementSpeed *= 1.6f;
        }
        else
        {
            isSprinting = false;
            movementSpeed /= 1.6f;
        }
    }

    public IEnumerator Dodge()
    {
        lastMove = moveDir;
        isDodging = true;
        yield return new WaitForSeconds(dodgeDuration);
        isDodging = false;
    }
}
