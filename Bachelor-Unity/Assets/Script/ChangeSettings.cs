using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSettings : MonoBehaviour
{

    public static bool IsPaused = false;
    public GameObject PauseUI;

    void Start()
    {
        PauseUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                print("Clicked after pause");
                Resume();
            }
            else
            {
                print("Clicked to pause");
                Pause();
            }

        }
    }

    public void Resume()
    {
        PauseUI.SetActive(false);
        IsPaused = false;
    }

    public void Pause()
    {
        PauseUI.SetActive(true);
        IsPaused = true;

    }
}
