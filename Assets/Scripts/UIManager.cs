using UnityEngine;
using TMPro;
using UnityEditor.Search;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private TextMeshProUGUI interactionMessage; // Mensaje de interacción

    public TextMeshProUGUI keyCountText; // Referencia al objeto de texto TextMeshPro
    public int keysNeeded = 2; // Cantidad de llaves necesarias para abrir la puerta

    void Start()
    {
        UpdateKeyCountText(); // Actualizar el texto al inicio
    }

    public void UpdateKeyCountText()
    {
        int keysRemaining = keysNeeded - Key.keysCollected; // Calcular la cantidad de llaves restantes
        if (keysRemaining > 0) // Verificar si faltan llaves
        {
            keyCountText.text = "Faltan " + keysRemaining + " llaves"; 
        }
        else
        {
            keyCountText.text = "La puerta se puede abrir";
        }
    }

    public void ShowMessage(string message)
    {
        // Mostrar el mensaje al jugador para indicarle que se ha activado un botón
        feedbackText.text = message;
        StartCoroutine(ClearMessageAfterDelay());
        Debug.Log("¡Se ha activado un botón!");
    }
    public void HideMessage()
    {
        interactionMessage.gameObject.SetActive(false);
    }
    private IEnumerator ClearMessageAfterDelay()
    {
        // Esperar 2 segundos antes de borrar el mensaje
        yield return new WaitForSeconds(2f);
        feedbackText.text = "";
    }
}


