using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed; // Velocidad  de movimiento del jugador
    [SerializeField] private float rotationSpeed; // Velocidad de rotaci�n del jugador
    [SerializeField] private bool isGrounded; // Variable para verificar si el jugador est� en el suelo
    [SerializeField] private float jumpHeight; // Altura del salto
    private float gravity = 9.8f; // Gravedad

    private CharacterController controller; // Referencia al CharacterController
    private Vector3 playerVelocity; // Velocidad del jugador
    private Transform groundCheck; // Transform para verificar el suelo
    private float groundCheckRadius = 0.2f; // Radio para verificar el suelo
    [SerializeField] private LayerMask groundMask; // M�scara para el suelo

    void Start()
    {
        controller = GetComponent<CharacterController>();
        groundCheck = new GameObject("GroundCheck").transform;
        groundCheck.SetParent(transform);
        groundCheck.localPosition = new Vector3(0, -controller.height / 2, 0);

        // Verificar si la escena actual es la escena de juego
        if (SceneManager.GetActiveScene().name == "Game") // Reemplaza "Game" con el nombre de tu escena de juego
        {
            // Ocultar el cursor
            Cursor.visible = false;

            // Bloquear el cursor en el centro de la pantalla
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void Update()
    {
        // Movimiento del jugador
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direccion = transform.right * horizontal + transform.forward * vertical;
        controller.Move(direccion * speed * Time.deltaTime);

        // Rotaci�n del jugador
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up * mouseX * rotationSpeed * Time.deltaTime);

        // Gravedad
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);
        Debug.Log("isGrounded: " + isGrounded); // Verificar si est� en el suelo

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Verificar si se presiona el bot�n de salto
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jump button pressed");
        }

        // Salto
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

        // Aplicar gravedad
        playerVelocity.y -= gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        Debug.Log("Player Velocity: " + playerVelocity); // Verificar la velocidad del jugador

        // Liberar el cursor y hacerlo visible si se presiona la tecla Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
