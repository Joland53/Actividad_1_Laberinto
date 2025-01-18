using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed; // Velocidad de movimiento del jugador
    [SerializeField] private float rotationSpeed; // Velocidad de rotación del jugador
    [SerializeField] private bool isGrounded; // Variable para verificar si el jugador está en el suelo
    [SerializeField] private float jumpHeight; // Altura del salto
    private float gravity = 9.8f; // Gravedad

    private CharacterController controller; // Referencia al CharacterController
    private Vector3 playerVelocity; // Velocidad del jugador
    private Transform groundCheck; // Transform para verificar el suelo
    private float groundCheckRadius = 0.2f; // Radio para verificar el suelo
    [SerializeField] private LayerMask groundMask; // Máscara para el suelo

    [SerializeField] private LayerMask buttonLayer; // Capa para los botones
    [SerializeField] private float raycastDistance = 3f; // Distancia máxima del raycast
    private Button currentButton; // Botón detectado actualmente

    [SerializeField] private TextMeshProUGUI pauseText; // Referencia al texto de pausa

    void Start()
    {
        InitializePlayer();
        ConfigureCursor();
        pauseText.gameObject.SetActive(false);
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleGravity();
        HandleJump();
        HandleCursorUnlock();
        HandleRaycast();
    }



    /// <summary>
    /// Lanza un raycast desde la cámara en la dirección en la que está mirando el jugador.
    /// </summary>
    private void HandleRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Raycast desde la cámara

        // Verificar si el raycast colisiona con un objeto de la capa de los botones
        if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance, buttonLayer))
        {

            // Verificar si el objeto colisionado tiene un componente Button
            if (hit.collider.TryGetComponent<Button>(out Button button))
            {
                currentButton = button;

                // Mostrar mensaje segun el estado del botón
                if (currentButton.IsActivated())
                {
                    FindObjectOfType<UIManager>().ShowMessage("Botón ya activado. La barrera está abierta ");
                }
                else
                {
                    FindObjectOfType<UIManager>().ShowMessage("Pulsa E para activar");
                }
                
            }

            // Activar el botón al presionar "E"
            if (currentButton != null && Input.GetKeyDown(KeyCode.E))
            {
                currentButton.ActivateButton(); // Llamar al método del botón
            }
        }
        else
        {
            currentButton = null; // No hay botón detectado
            FindObjectOfType<UIManager>().HideMessage(); // Ocultar mensaje
        }
    }

    /// <summary>
    /// Inicializa el CharacterController y el groundCheck.
    /// </summary>
    private void InitializePlayer()
    {
        controller = GetComponent<CharacterController>();
        groundCheck = new GameObject("GroundCheck").transform;
        groundCheck.SetParent(transform);
        groundCheck.localPosition = new Vector3(0, -controller.height / 2, 0);
    }

    /// <summary>
    /// Configura el cursor según la escena.
    /// </summary>
    private void ConfigureCursor()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    /// <summary>
    /// Maneja el movimiento del jugador.
    /// </summary>
    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direccion = transform.right * horizontal + transform.forward * vertical;
        controller.Move(direccion * speed * Time.deltaTime);
    }

    /// <summary>
    /// Maneja la rotación del jugador.
    /// </summary>
    private void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up * mouseX * rotationSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Maneja la gravedad y la verificación del suelo.
    /// </summary>
    private void HandleGravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);
        Debug.Log("isGrounded: " + isGrounded); // Verificar si está en el suelo

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        playerVelocity.y -= gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

    }

    /// <summary>
    /// Maneja el salto del jugador.
    /// </summary>
    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jump button pressed");
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Debug.Log("Jump pressed and isGrounded"); // Verificar si se presiona el salto
            if (jumpHeight > 0 && gravity > 0)
            {
                playerVelocity.y = Mathf.Sqrt(jumpHeight * 2.0f * gravity);
                Debug.Log("Jump velocity: " + playerVelocity.y); // Verificar la velocidad del salto
            }
            else
            {
                Debug.LogError("jumpHeight y gravity deben ser mayores que 0");
            }
        }
    }

    /// <summary>
    /// Maneja la liberación y el bloqueo del cursor cuando se presiona la tecla Escape.
    /// También pausa y reanuda el juego.
    /// </summary>
    private void HandleCursorUnlock()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0f; // Pausa el juego
                pauseText.gameObject.SetActive(true); // Muestra el texto de pausa
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1f; // Reanuda el juego
                pauseText.gameObject.SetActive(false); // Oculta el texto de pausa
            }
        }
    }
}
