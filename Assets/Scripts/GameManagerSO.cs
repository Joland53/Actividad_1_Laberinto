using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MiGameManagerSO")]
public class GameManagerSO : ScriptableObject
{
    public event Action OnButtonPressed;
    public void PressButtom(int idButton)
    {
        //Lanzar evento de que un botón ha sido pulsado
        OnButtonPressed?.Invoke();
        Debug.Log("Presionando el boton " + idButton);
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
