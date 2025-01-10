using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttom : MonoBehaviour
{
    [SerializeField] private GameManagerSO gM;
    [SerializeField] private int idButton;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
        {
            //Abrir los barrotes
            gM.PressButtom(idButton);
        }
    }

}
