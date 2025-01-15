using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] private GameManagerSO gM;
    [SerializeField] public int idTrap;

    private bool isOpen = false;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveDuration;

    // Start is called before the first frame update
    void Start()
    {
        //gM.OnButtonPressed += ActivateTrap;
        if (gM != null)
        {
            gM.OnButtonPressed += ActivateTrap;
            Debug.Log($"Trampa {idTrap} registrada para eventos de botón.");
        }
        else
        {
            Debug.LogError("GameManagerSO no asignado en SpikeTrap.");
        }

    }

    // Método para activar la trampa
    private void ActivateTrap()
    {
        Debug.Log($"Intentando activar trampa {idTrap}. CurrentButtonID: {gM.CurrentButtonID}");
        if (gM.CurrentButtonID == idTrap)
        {
            isOpen = true;
            Debug.Log("Activando la trampa" + idTrap);
            StartCoroutine(StopTrap());
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen)
        {
            //Mover la trampa hacia adelante
            transform.Translate(Vector3.forward * 1 * Time.deltaTime);
            Debug.Log($"Trampa {idTrap} moviéndose.");

        }

    }

    // Corrutina para detener la trampa después de un tiempo
    private IEnumerator StopTrap()
    {
        yield return new WaitForSeconds(moveDuration);
        isOpen = false;
        Debug.Log($"Trampa {idTrap} detenida.");
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
}
