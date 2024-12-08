using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI keyCountText; // Referencia al objeto de texto TextMeshPro
    public int keysNeeded = 2; // Cantidad de llaves necesarias para abrir la puerta

    void Start()
    {
        UpdateKeyCountText(); // Actualizar el texto al inicio
    }

    public void UpdateKeyCountText()
    {
        int keysRemaining = keysNeeded - Key.keysCollected;
        if (keysRemaining > 0)
        {
            keyCountText.text = "Faltan " + keysRemaining + " llaves";
        }
        else
        {
            keyCountText.text = "La puerta se puede abrir";
        }
    }
}


