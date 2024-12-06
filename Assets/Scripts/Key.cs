using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public static int keysCollected = 0; // Variable para almacenar las llaves recolectadas

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verificar si el objeto que colisiona es el jugador
        {
            keysCollected++; // Aumentar el contador de llaves recolectadas
            Debug.Log("Llaves recolectadas: " + keysCollected); // Mostrar en consola las llaves recolectadas
            Destroy(gameObject); // Destruir la llave
        }
    }
}
