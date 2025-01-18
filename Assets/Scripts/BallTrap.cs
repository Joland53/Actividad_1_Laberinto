using UnityEngine;
using UnityEngine.SceneManagement;

public class BallTrap : MonoBehaviour
{
    [SerializeField] private GameManagerSO gM; // Referencia al GameManagerSO
    [SerializeField] private int idTrap; // ID de la trampa asociada
    [SerializeField] private float moveSpeed = 5f; // Velocidad de avance
    [SerializeField] private float ballRadius = 0.5f; // Radio de la bola
    [SerializeField] private float maxDistance = 100f; // Distancia máxima antes de destruir la bola

    private Vector3 initialPosition; // Para calcular la distancia recorrida
    private bool isRun = false;

    private AudioSource moveSoundTrap; // Referencia al componente AudioSource
    void Start()
    {
        initialPosition = transform.position; // Guardar la posición inicial
        gM.OnTrapTriggered += ActivateTrapBall; // Suscribirse al evento del GameManagerSO

        moveSoundTrap = GetComponent<AudioSource>(); // Obtener el componente AudioSource del objeto trampa
    }

    private void ActivateTrapBall(int id)
    {
        if (id == idTrap) // Verificar si el ID coincide con el de esta trampa
        {
            isRun = true;
            Debug.Log($"Trampa de bola {idTrap} activada.");

            if (moveSoundTrap != null)
            {
                moveSoundTrap.Play(); // Reproducir el sonido de la trampa
            }
        }
    }

    void Update()
    {
        if (isRun)
        {
            // Movimiento forzado hacia adelante en el mundo
            Vector3 forwardMovement = new Vector3(1, 0, 0) * moveSpeed * Time.deltaTime;
            transform.position += forwardMovement;

            // Rotación sincronizada con el movimiento
            float rotationAmount = (forwardMovement.magnitude / (2 * Mathf.PI * ballRadius)) * 360f; // Convertir distancia a grados
            transform.Rotate(Vector3.left, rotationAmount);

            // Destruir la bola después de cierta distancia
            float totalDistance = Vector3.Distance(initialPosition, transform.position);
            if (totalDistance > maxDistance)
            {
                Debug.Log("Bola destruida tras alcanzar la distancia máxima.");
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trampa {idTrap} tocó: {other.transform.name}");

        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador tocó la trampa. Cargando escena GameOver.");
            // Liberar el cursor y hacerlo visible
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("GameOver");
        }
    }
    private void OnDestroy()
    {
        // Desuscribirse del evento para evitar errores
        gM.OnTrapTriggered -= ActivateTrapBall;
    }
}
