using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Key : MonoBehaviour
{
    public static int keysCollected = 0; // Variable para almacenar las llaves recolectadas
    private UIManager uiManager; // Referencia al UIManager
    private AudioSource keySound; // Referencia al componente AudioSource

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>(); // Encontrar el UIManager en la escena
        keySound = GetComponent<AudioSource>(); // Obtener el componente AudioSource del objeto llave
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verificar si el objeto que colisiona es el jugador
        {
            keysCollected++; // Aumentar el contador de llaves recolectadas
            if (keySound != null) // Asegurarse de que hay un AudioSource asignado
            {
                keySound.Play(); // Reproducir el sonido de la llave
            }

            Debug.Log("Llaves recolectadas: " + keysCollected); // Mostrar en consola las llaves recolectadas
            uiManager.UpdateKeyCountText(); // Actualizar el texto en la interfaz de usuario

            // Desactivar el objeto para que no interfiera mientras suena el sonido
            GetComponent<Collider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;

            // Destruir el objeto después de que el sonido termine
            Destroy(gameObject, keySound.clip.length);
        }
    }

    // Método para reiniciar la variable keysCollected al cargar la escena de juego
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void OnSceneLoaded()
    {
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            if (scene.name == "Game") // Verificar si la escena cargada es la de juego
            {
                keysCollected = 0;
            }
        };
    }
}
