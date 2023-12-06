using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public CharacterController characterController;

    public float speed = 10.0f;
    public float sprintSpeedMultiplier = 2.0f;
    public float turnSmoothTime = 0.5f;
    private float turnSmoothVelocity;

    public float jumpSpeed = 8.0f;
    public float gravity = -9.81f;

    private Vector3 velocity;
    private bool isGrounded;
    private bool isJumping;

    public AudioClip jumpSound;
    private AudioSource audioSource;

    public int maxJumpCount = 2;
    private int jumpCount;

    public float coyoteTimeDuration = 0.2f;
    private float coyoteTimeCounter;

    [Header("Movimiento")]
    public Transform cam;

    [Header("Animaciones")]
    public Animator animator;

    private Transform currentPlatform;
    private Vector3 platformPositionLastFrame;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource no encontrado en el objeto, se requiere para reproducir sonidos.");
        }

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        isGrounded = characterController.isGrounded;
        if (isGrounded && currentPlatform == null)
        {
            coyoteTimeCounter = coyoteTimeDuration;
            jumpCount = 0;
            velocity.y = -2f;
            isJumping = false;
            animator.SetBool("IsJumping", false);
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && (isGrounded || coyoteTimeCounter > 0 || jumpCount < maxJumpCount))
        {
            DetachFromPlatform();

            velocity.y = Mathf.Sqrt(jumpSpeed * -2f * gravity);
            jumpCount++;
            isJumping = true;
            animator.SetBool("IsJumping", true);
            PlayJumpSound();
        }

        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }

        HandleMovement();

        if (currentPlatform != null)
        {
            Vector3 deltaMovement = currentPlatform.position - platformPositionLastFrame;
            characterController.Move(deltaMovement);
            platformPositionLastFrame = currentPlatform.position;
        }
    }

    void HandleMovement()
    {
        float H_axis = Input.GetAxis("Horizontal");
        float V_axis = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(H_axis, 0, V_axis).normalized;

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRunning ? speed * sprintSpeedMultiplier : speed;

        animator.SetBool("Running", direction.magnitude >= 0.1f && isGrounded);
        animator.SetBool("IsDoubleRunning", isRunning);

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveDir.normalized * currentSpeed * Time.deltaTime);
        }

        characterController.Move(velocity * Time.deltaTime);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("MovingPlatform") && isGrounded)
        {
            AttachToPlatform(hit.transform);
        }
    }

    void AttachToPlatform(Transform platform)
    {
        if (currentPlatform != platform)
        {
            transform.parent = platform;
            currentPlatform = platform;
            platformPositionLastFrame = platform.position;
        }
    }

    void DetachFromPlatform()
    {
        if (currentPlatform != null)
        {
            transform.parent = null;
            currentPlatform = null;
        }
    }

    public bool IsFalling()
    {
        return isJumping && velocity.y < 0;
    }

    void PlayJumpSound()
    {
        if (jumpSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }
    }

    public void EstablecerPosicion(Vector3 nuevaPosicion)
    {
        characterController.enabled = false;
        transform.position = nuevaPosicion;
        characterController.enabled = true;
    }
}
