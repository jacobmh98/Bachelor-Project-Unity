using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;
using System.IO;

public class ChooseMenu : MonoBehaviour
{
    public GameObject inputField;
    public Image panel;
    Controller cont = Controller.getInstance();
    public GameObject chooseMenu;
    public GameObject optionsMenu;


    public Sprite sprite;

    private void Start()
    {
        if (cont.backFromPoints)
        {
            cont.backFromPoints = false;
            chooseMenu.SetActive(false);
            optionsMenu.SetActive(true);
        }
    }

    public void RunVisualsWithPath()
    {
        print(Application.dataPath + @"/../7k_data_extracted_rotated.json");
        var parentPath = Directory.GetParent(Application.dataPath);
        print(parentPath);
        StateNameController.SevenKPath = inputField.GetComponent<TMP_InputField>().text;
        string path = StateNameController.SevenKPath;
        print(StateNameController.SevenKPath);
        Process p = new Process();
        p.StartInfo = new ProcessStartInfo();
        p.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
        p.StartInfo.FileName = Application.streamingAssetsPath + @"/jsonload.exe";
        p.StartInfo.Arguments = path;
        p.Start();
        p.WaitForExit();
        print(parentPath + "\\7k_data_extracted_rotated.json");

        cont.setPath(parentPath + "\\7k_data_extracted_rotated.json");

        cont.LoadController();
    
    }

    public void changeBackground()
    {
        panel.sprite = sprite;
    }

    public void quitButton()
    {
        print("QUIT PROGRAM!");
        Application.Quit();
    }

}
