using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsButtons : MonoBehaviour
{
    DataBase db = DataBase.getInstance();
    public void letsGoBack()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        db.setBackFromPoints(true);
    }
}
