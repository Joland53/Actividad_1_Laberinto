using UnityEngine;
using UnityEngine.SceneManagement;

public class WallSection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Objeto golpeado: {other.transform.name}"); // Depuraci�n

        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador golpeado por una secci�n del muro. Cargando escena GameOver.");
            // Liberar el cursor y hacerlo visible
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("GameOver"); // Cargar la escena de Game Over
        }
    }
}

