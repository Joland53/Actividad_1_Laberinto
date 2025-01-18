using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private GameManagerSO gM;
    [SerializeField] private int pressurePlateID;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"Placa de presi�n {pressurePlateID} activada.");
            gM.TriggerTrap(pressurePlateID);
        }
    }


}
