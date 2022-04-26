using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    Controller cont = Controller.getInstance();
    public void RunVisuals()
    {
        cont.PointLoader();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void QuitProgram()
    {
        print("QUIT PROGRAM!");
        Application.Quit();
    }

}
