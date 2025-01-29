using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private GameManagerSO gM;
    [SerializeField] public int idButton;
    private bool isActivated = false;
    private AudioSource buttonSound;
    private void Start()
    {
        buttonSound = GetComponent<AudioSource>(); // Obtener el componente AudioSource del objeto botón
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
        {
            //Abrir los barrotes
            gM.RegisterPressButton(idButton);
        }
    }

    public void ActivateButton() // Método para activar el botón
    {
        if (!isActivated) // Si el botón no está activado
        {
            isActivated = true; // Activar el botón

            if (buttonSound != null) // Asegurarse de que hay un AudioSource asignado
            {
                buttonSound.Play(); // Reproducir el sonido del botón
            }
            //Abrir los barrotes
            gM.RegisterPressButton(idButton); // Llamar al método RegisterPressButtom del GameManagerSO
            Debug.Log($"Botón {idButton} activado.");

        }

    }

    public bool IsActivated()
    {
        return isActivated; // Retorna el estado del botón
    }

}