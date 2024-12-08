using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public static int keysCollected = 0; // Variable para almacenar las llaves recolectadas
    private UIManager uiManager; // Referencia al UIManager
    private AudioSource keySound; // Referencia al componente AudioSource

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>(); // Encontrar el UIManager en la escena
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verificar si el objeto que colisiona es el jugador
        {
            keysCollected++; // Aumentar el contador de llaves recolectadas
            keySound.Play(); // Reproducir el sonido de la llave
            Debug.Log("Llaves recolectadas: " + keysCollected); // Mostrar en consola las llaves recolectadas
            uiManager.UpdateKeyCountText(); // Actualizar el texto en la interfaz de usuario
            Destroy(gameObject); // Destruir la llave
        }
    }
}
