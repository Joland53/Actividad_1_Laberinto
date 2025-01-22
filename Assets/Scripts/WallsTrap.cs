using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WallsTrap : MonoBehaviour
{
    [SerializeField] private GameManagerSO gM;
    [SerializeField] public int idTrap;
    [SerializeField] private GameObject[] wallsSections;
    [SerializeField] private float fallDistance; // Distancia que caer�n las secciones de muro
    [SerializeField] private float fallSpeed; // Velocidad a la que caer�n las secciones de muro
    [SerializeField] private float delayBetweenFalls; // Tiempo de espera entre ca�das de secciones de muro
    [SerializeField] private float resetDelay; // Tiempo antes de que la secci�n vuelva a su posici�n original

    private Vector3[] initialPositions; // Arreglo para almacenar las posiciones iniciales de las secciones de muro
    private bool isTrapActive = false; // Variable para evtar que la trampa se active m�s de una vez
    private AudioSource fallSoundTrap; // Referencia al componente AudioSource
    private bool playerInside = false; // Variable para verificar si el jugador est� dentro del �rea de la trampa

    // Start is called before the first frame update
    void Start()
    {
        //Almacenar las posiciones iniciales de las secciones de muro
        initialPositions = new Vector3[wallsSections.Length];
        for (int i = 0; i < wallsSections.Length; i++)
        {
            initialPositions[i] = wallsSections[i].transform.position;
        }

        //Registrar el m�todo ActivateTrap en el evento OnTrapTriggered del GameManagerSO
        gM.OnTrapTriggered += ActivateTrap;

        fallSoundTrap = GetComponent<AudioSource>(); // Obtener el componente AudioSource del objeto trampa
    }

    private void OnTriggerEnter(Collider other)
    {
        //Verificar si el objeto que colisiona es el jugador y si la trampa no ha sido activada
        if (other.CompareTag("Player") && !isTrapActive)
        {
            
            Debug.Log($"Jugador entr� en el �rea de la trampa {idTrap}. Notificando al GameManager.");
            gM.TriggerTrap(idTrap); // Notificar al GameManagerSO para que active la trampa
            playerInside = true; // Indicar que el jugador est� dentro del �rea de la trampa
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Verificar si el objeto que sale es el jugador y si la trampa no ha sido activada
        if (other.CompareTag("Player"))
        {
            playerInside = false; // Indicar que el jugador ya no est� dentro del �rea de la trampa
        }
    }

    private void ActivateTrap(int id)
    {
        if (id == idTrap) // Verificar si el ID coincide con el de esta trampa
        {
            Debug.Log($"Trampa {idTrap} activada por el GameManager.");
            isTrapActive = true; // Evitar m�ltiples activaciones
            StartCoroutine(ExecuteTrap());
        }
    }

    private IEnumerator ExecuteTrap()
    {
        while (playerInside)
        {
            for (int i = 0; i < wallsSections.Length; i++)
            {
                if (fallSoundTrap != null) // Asegurarse de que hay un AudioSource asignado
                {
                    fallSoundTrap.Play(); // Reproducir el sonido de la trampa
                }
                // Hacer que la secci�n actual caiga
                yield return StartCoroutine(FallAndRise(wallsSections[i], initialPositions[i]));
                yield return new WaitForSeconds(delayBetweenFalls); // Esperar antes de la siguiente ca�da

            }

        }
        isTrapActive = false; // Permitir que la trampa se reactive si es necesario
    }

    private IEnumerator FallAndRise(GameObject section, Vector3 initialPosition)
    {
        // Calcular la posici�n final hacia donde caer�
        Vector3 targetPosition = initialPosition - new Vector3(0, fallDistance, 0);

        // Hacer que la secci�n caiga
        while (Vector3.Distance(section.transform.position, targetPosition) > 0.1f)
        {
            section.transform.position = Vector3.MoveTowards(section.transform.position, targetPosition, fallSpeed * Time.deltaTime);
            yield return null;
        }

        // Esperar un momento antes de que la secci�n se levante
        yield return new WaitForSeconds(resetDelay);

        // Hacer que la secci�n vuelva a su posici�n original
        while (Vector3.Distance(section.transform.position, initialPosition) > 0.1f)
        {
            section.transform.position = Vector3.MoveTowards(section.transform.position, initialPosition, fallSpeed * Time.deltaTime);
            yield return null;
        }
    }
    private void OnDestroy()
    {
        // Desuscribirse del evento para evitar errores
        gM.OnTrapTriggered -= ActivateTrap;
    }
}
