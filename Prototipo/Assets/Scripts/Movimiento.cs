using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public CharacterController characterController;

    private Vector3 startPosition;
    public float speed = 10.0f;
    public float turnSmoothTime = 0.5f;
    private float turnSmoothVelocity;

    public float jumpSpeed = 8.0f;
    public float gravity = -9.81f;

    private Vector3 velocity;
    private bool isGrounded;

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
        startPosition = transform.position;
    }

    void Update()
    {
 
        bool wasGrounded = isGrounded;
        isGrounded = characterController.isGrounded;

        // Start or reset the Coyote Time counter when grounded
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTimeDuration;
        }
        else if (wasGrounded && !isGrounded)
        {
            // Player just left the ground, start Coyote Time
            coyoteTimeCounter = coyoteTimeDuration;
        }
        else
        {
            // Player is in the air, reduce Coyote Time
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
        // Esta función se llamará automáticamente cuando el jugador entre en un trigger.
      
       

        // Allow jump if Coyote Time is active
        if (Input.GetButtonDown("Jump") && coyoteTimeCounter > 0)
        {
            animator.SetBool("IsJumping", true);
            velocity.y = Mathf.Sqrt(jumpSpeed * -2 * gravity);
            coyoteTimeCounter = 0; // Reset Coyote Time after jumping
        }

        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }

        if (direction.magnitude >= 0.1f && V_axis > 0)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        Vector3 moveDir = Quaternion.Euler(0f, cam.eulerAngles.y, 0f) * direction;
        characterController.Move(moveDir * speed * Time.deltaTime);

        animator.SetBool("Running", direction.magnitude >= 0.1f);

        characterController.Move(velocity * Time.deltaTime);
    }
  
}
