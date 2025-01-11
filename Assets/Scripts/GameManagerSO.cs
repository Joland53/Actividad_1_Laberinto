using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MiGameManagerSO")]
public class GameManagerSO : ScriptableObject
{
    public int CurrentButtonID { get; internal set; } // ID del bot�n actual
    public event Action OnButtonPressed; // Evento para cuando se presiona un bot�n
    public void RegisterPressButton(int idButton)
    {
        //Lanzar evento de que un bot�n ha sido pulsado
        CurrentButtonID = idButton;
        OnButtonPressed?.Invoke();
        Debug.Log("Presionando el boton {idButton} ");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
