using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public CharacterController characterController;

    public float speed = 10.0f;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    public float jumpSpeed = 8.0f;
    public float gravity = -9.81f;

    private Vector3 velocity;
    private bool isGrounded;

    [Header("Movimiento")]
    public Transform cam;

    [Header("Animaciones")]
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = characterController.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Se establece un pequeño valor negativo para asegurar que el personaje está firmemente sobre el suelo.
            animator.SetBool("IsJumping", false);
        }

        float H_axis = Input.GetAxis("Horizontal");
        float V_axis = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(H_axis, 0, V_axis).normalized;

        // Comprobar si el personaje debe saltar
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            animator.SetBool("IsJumping", true); // Se activa la animación de salto
            velocity.y = Mathf.Sqrt(jumpSpeed * -2 * gravity);
        }

        // Aplicar la gravedad constantemente
        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }

        // Mover el jugador
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveDir * speed * Time.deltaTime);
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }

        // Se mueve el personaje independientemente de si está saltando o no
        characterController.Move(velocity * Time.deltaTime);
    }
}
