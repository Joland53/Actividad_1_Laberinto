using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bars : MonoBehaviour
{
    [SerializeField] private GameManagerSO gM;
    [SerializeField] private int idBar;

    private bool isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        gM.OnButtonPressed += OpenBars;
    }

    private void OpenBars()
    {
        isOpen = true;
        Debug.Log("Abriendo las rejas " + idBar);
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen)
        {
            //Abrir las rejas hacia abajo
            transform.Translate(Vector3.down * 5 * Time.deltaTime);
        }
    }
}
