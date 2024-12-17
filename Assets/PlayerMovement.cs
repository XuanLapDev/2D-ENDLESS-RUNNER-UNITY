using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpForce = 30f; // Tăng giá trị jumpForce để nhảy cao hơn
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform feetPos;
    [SerializeField] private float groundDistance = 0.25f;

    [SerializeField] private BoxCollider2D playerCollider; // Gán Collider là BoxCollider2D
    [SerializeField] private Vector2 normalColliderSize = new Vector2(1f, 1f); // Kích thước bình thường
    [SerializeField] private Vector2 crouchColliderSize = new Vector2(1f, 0.5f); // Kích thước khi cúi

    private bool isGrounded = false;
    private bool isJumping = false;
    private bool isCrouching = false;

    private float jumpTimeCounter;
    private float maxJumpTime = 0.4f; // Tăng giá trị này nếu cần thời gian nhảy lâu hơn

    private Vector3 originalPosition; // Lưu vị trí ban đầu của nhân vật

    private void Start()
    {
        originalPosition = transform.position; // Lưu lại vị trí ban đầu của nhân vật
        rb.gravityScale = 2f; // Điều chỉnh gravity scale để kiểm soát tốc độ rơi
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, groundDistance, groundLayer);

        // Logic nhảy
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
            isJumping = true;
            jumpTimeCounter = maxJumpTime;
        }

        if (Input.GetButton("Jump") && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                // Thay đổi vận tốc với velocity để làm cho nhân vật nhảy cao hơn
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }

        // Logic cúi xuống (Dùng Input.GetButton thay vì Input.GetButtonDown)
        if (Input.GetButton("Crouch") && isGrounded && !isCrouching)  // Giữ phím "Crouch"
        {
            Crouch();
        }
        else if (!Input.GetButton("Crouch") && isCrouching) // Thả phím "Crouch"
        {
            StandUp();
        }
    }

    private void Jump()
    {
        // Sử dụng velocity để thay đổi vận tốc của nhân vật, thay vì linearVelocity
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); 
    }

    private void Crouch()
    {
        isCrouching = true;
        playerCollider.size = crouchColliderSize; // Giảm kích thước Collider khi cúi

        // Đặt vị trí nhân vật xuống để tạo cảm giác cúi
        transform.position = new Vector3(originalPosition.x, originalPosition.y - 0.5f, originalPosition.z);

        // Quay nhân vật để nằm ngang (quay 90 độ)
        transform.rotation = Quaternion.Euler(0, 0, 90); 

        Debug.Log("Player is crouching.");
    }

    private void StandUp()
    {
        isCrouching = false;
        playerCollider.size = normalColliderSize; // Trả về kích thước Collider bình thường

        // Đưa nhân vật trở về vị trí ban đầu
        transform.position = originalPosition;

        // Quay lại vị trí thẳng đứng (quay 0 độ)
        transform.rotation = Quaternion.Euler(0, 0, 0); 

        Debug.Log("Player stood up.");
    }
}