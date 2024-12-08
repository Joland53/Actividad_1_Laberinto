using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float jumpHeight;
    private float gravity = 9.8f;

    private CharacterController controller;
    private Vector3 playerVelocity;
    //private float groundCheckDistance = 0.4f; // Aumentar la distancia para verificar el suelo
    private Transform groundCheck; // Transform para verificar el suelo
    private float groundCheckRadius = 0.2f; // Radio para verificar el suelo
    [SerializeField] private LayerMask groundMask; // Máscara para el suelo

    void Start()
    {
        controller = GetComponent<CharacterController>();
        groundCheck = new GameObject("GroundCheck").transform;
        groundCheck.SetParent(transform);
        groundCheck.localPosition = new Vector3(0, -controller.height / 2, 0);
    }

    void Update()
    {
        // Movimiento del jugador
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direccion = transform.right * horizontal + transform.forward * vertical;
        controller.Move(direccion * speed * Time.deltaTime);

        // Rotación del jugador
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up * mouseX * rotationSpeed * Time.deltaTime);

        // Gravedad
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);
        Debug.Log("isGrounded: " + isGrounded); // Verificar si está en el suelo

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Verificar si se presiona el botón de salto
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
    }
}
