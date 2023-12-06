using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public CharacterController characterController;

    public float speed = 10.0f;
    public float sprintSpeedMultiplier = 2.0f; // Multiplicador de velocidad al correr
    public float turnSmoothTime = 0.5f;
    private float turnSmoothVelocity;

    public float jumpSpeed = 8.0f;
    public float gravity = -9.81f;

    private Vector3 velocity;
    private bool isGrounded;
    private bool isJumping;

    public AudioClip jumpSound;
    private AudioSource audioSource;

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

    private Transform currentPlatform; // Para almacenar la plataforma actual

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>(); // Obtener el componente AudioSource

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
            jumpCount = 0; // Restablece el contador de saltos cuando el jugador toca el suelo
            velocity.y = -2f; // Pequeña fuerza hacia abajo para asegurar que el controlador está en el suelo
            isJumping = false;
            animator.SetBool("IsJumping", false);
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        float H_axis = Input.GetAxis("Horizontal");
        float V_axis = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(H_axis, 0, V_axis).normalized;

        if (Input.GetButtonDown("Jump") && (isGrounded || coyoteTimeCounter > 0 || jumpCount < maxJumpCount))
        {
            if (currentPlatform != null)
            {
                transform.parent = null; // Desvincula al jugador de la plataforma
                currentPlatform = null;
            }

            velocity.y = Mathf.Sqrt(jumpSpeed * -2f * gravity);
            jumpCount++; // Incrementa el contador de saltos
            coyoteTimeCounter = 0; // Restablece el coyote time
            isJumping = true;
            animator.SetBool("IsJumping", true);
            PlayJumpSound();
        }

        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }

        // Verifica si el jugador está corriendo y actualiza la velocidad y la animación correspondientemente
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
            if (currentPlatform != hit.transform)
            {
                transform.parent = hit.transform; // Hace al jugador hijo de la plataforma
                currentPlatform = hit.transform;
            }
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
    characterController.enabled = false; // Desactivar temporalmente para mover
    transform.position = nuevaPosicion;
    characterController.enabled = true; // Reactivar el controlador
}
}
