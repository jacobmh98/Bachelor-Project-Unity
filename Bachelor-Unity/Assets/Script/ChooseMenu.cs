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
    Controller controller = Controller.getInstance();
    DataBase db = DataBase.getInstance();

    public GameObject inputField;
    public Image panel;
    public GameObject chooseMenu;
    public GameObject optionsMenu;
    public Sprite sprite;

    Toggle tgToggle;
    Toggle nnToggle;
    Toggle odToggle;

    private void Start()
    {
        if (controller.backFromPoints)
        {
            controller.backFromPoints = false;
            chooseMenu.SetActive(false);
            optionsMenu.SetActive(true);
            tgToggle = GameObject.Find("TriangulationToggle").GetComponent<Toggle>();
            nnToggle = GameObject.Find("NNToggle").GetComponent<Toggle>();
            odToggle = GameObject.Find("ODToggle").GetComponent<Toggle>();
            tgToggle.isOn = db.getTriangulationEnabled();
            nnToggle.isOn = db.getNearestNeighbourEnabled();
            odToggle.isOn = db.getOutlierHeightEnabled();
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

        controller.setPath(parentPath + "\\7k_data_extracted_rotated.json");

        print("ChooseMenu calls controller.LoadController");
        controller.LoadController();
    
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
