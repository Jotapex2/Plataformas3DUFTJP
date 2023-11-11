using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public CharacterController characterController;

    public float speed = 10.0f;
    public float turnSmoothTime = 0.5f;
    private float turnSmoothVelocity;

    public float jumpSpeed = 8.0f;
    public float gravity = -9.81f;

    private Vector3 velocity;
    private bool isGrounded;

    // Variables para el doble salto
    public int maxJumpCount = 2; // Número máximo de saltos permitidos
    private int jumpCount; // Contador actual de saltos

    // Coyote Time variables
    public float coyoteTimeDuration = 0.2f;
    private float coyoteTimeCounter;

    [Header("Movimiento")]
    public Transform cam;

    [Header("Animaciones")]
    public Animator animator;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        bool wasGrounded = isGrounded;
        isGrounded = characterController.isGrounded;

        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTimeDuration;
            jumpCount = 0; // Restablece el contador de saltos cuando el jugador toca el suelo
        }
        else if (wasGrounded && !isGrounded)
        {
            coyoteTimeCounter = coyoteTimeDuration;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            animator.SetBool("IsJumping", false);
        }

        float H_axis = Input.GetAxis("Horizontal");
        float V_axis = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(H_axis, 0, V_axis).normalized;

        if (Input.GetButtonDown("Jump") && (isGrounded || coyoteTimeCounter > 0 || jumpCount < maxJumpCount))
        {
            animator.SetBool("IsJumping", true);
            velocity.y = Mathf.Sqrt(jumpSpeed * -2f * gravity);
            jumpCount++; // Incrementa el contador de saltos
            coyoteTimeCounter = 0; // Restablece el coyote time
        }

        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }

        float currentSpeed = speed;
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveDir.normalized * currentSpeed * Time.deltaTime);
        }

        characterController.Move(velocity * Time.deltaTime);

        // Actualiza las animaciones
        animator.SetBool("Running", direction.magnitude >= 0.1f);
    }
}
