using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public int keysRequired = 2; // Variable para almacenar las llaves requeridas

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verificar si el objeto que colisiona es el jugador
        {
            if (Key.keysCollected >= keysRequired) // Verificar si el jugador tiene las llaves requeridas
            {
                Debug.Log("Puerta abierta"); // Mostrar en consola que la puerta se abrió
                Destroy(gameObject); // Destruir la puerta
                SceneManager.LoadScene("End"); // Cargar la escena de victoria
            }
            else
            {
                Debug.Log("Faltan llaves"); // Mostrar en consola que faltan llaves
            }

        }
    }
}
