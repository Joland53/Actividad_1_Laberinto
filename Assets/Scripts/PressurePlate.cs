using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private GameManagerSO gM;
    [SerializeField] private int pressurePlateID;
    private AudioSource pressurePlateSound;
    
    private void Start()
    {
        pressurePlateSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"Placa de presión {pressurePlateID} activada.");
            gM.TriggerTrap(pressurePlateID);
            if (pressurePlateSound != null)
            {
                pressurePlateSound.Play();
            }
        }
    }


}
