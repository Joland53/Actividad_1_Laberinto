using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // M�todo para cargar la escena de juego
    public void PlayAgain()
    {
        SceneManager.LoadScene("Game"); 

    }

    // M�todo para salir del juego
    public void QuitGame()
    {
        Application.Quit();
    }
}
