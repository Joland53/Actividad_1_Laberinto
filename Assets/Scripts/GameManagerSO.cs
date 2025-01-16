using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MiGameManagerSO")]
public class GameManagerSO : ScriptableObject
{
    public int CurrentButtonID { get; internal set; } // ID del bot�n actual
    public event Action OnButtonPressed; // Evento para cuando se presiona un bot�n
    public event Action<int> OnTrapTriggered; // Evento para cuando se activa una trampa
    public void RegisterPressButton(int idButton)
    {
        //Lanzar evento de que un bot�n ha sido pulsado
        CurrentButtonID = idButton;
        OnButtonPressed?.Invoke();
        Debug.Log("Presionando el bot�n {idButton} ");
    }

    public void TriggerTrap(int idTrap)
    {
        //Lanzar evento de que una trampa ha sido activada
        Debug.Log($"Activando trampa {idTrap}");
        OnTrapTriggered?.Invoke(idTrap);
    }
}
