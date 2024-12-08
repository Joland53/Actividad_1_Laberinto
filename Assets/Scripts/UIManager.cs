using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI keyCountText; // Referencia al objeto de texto TextMeshPro

    void Start()
    {
        UpdateKeyCountText(); // Actualizar el texto al inicio
    }

    public void UpdateKeyCountText()
    {
        keyCountText.text = "Llaves recolectadas: " + Key.keysCollected;
    }
}

