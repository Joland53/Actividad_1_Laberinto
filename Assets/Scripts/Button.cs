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
        buttonSound = GetComponent<AudioSource>(); // Obtener el componente AudioSource del objeto bot�n
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
        {
            //Abrir los barrotes
            gM.RegisterPressButton(idButton);
        }
    }

    public void ActivateButton() // M�todo para activar el bot�n
    {
        if (!isActivated) // Si el bot�n no est� activado
        {
            isActivated = true; // Activar el bot�n

            if (buttonSound != null) // Asegurarse de que hay un AudioSource asignado
            {
                buttonSound.Play(); // Reproducir el sonido del bot�n
            }
            //Abrir los barrotes
            gM.RegisterPressButton(idButton); // Llamar al m�todo RegisterPressButtom del GameManagerSO
            Debug.Log($"Bot�n {idButton} activado.");

        }

    }

    public bool IsActivated()
    {
        return isActivated; // Retorna el estado del bot�n
    }

}