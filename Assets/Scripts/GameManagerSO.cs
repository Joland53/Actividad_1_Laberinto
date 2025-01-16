using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MiGameManagerSO")]
public class GameManagerSO : ScriptableObject
{
    public int CurrentButtonID { get; internal set; } // ID del botón actual
    public event Action OnButtonPressed; // Evento para cuando se presiona un botón
    public event Action<int> OnTrapTriggered; // Evento para cuando se activa una trampa
    public void RegisterPressButton(int idButton)
    {
        //Lanzar evento de que un botón ha sido pulsado
        CurrentButtonID = idButton;
        OnButtonPressed?.Invoke();
        Debug.Log("Presionando el botón {idButton} ");
    }

    public void TriggerTrap(int idTrap)
    {
        //Lanzar evento de que una trampa ha sido activada
        Debug.Log($"Activando trampa {idTrap}");
        OnTrapTriggered?.Invoke(idTrap);
    }
}
