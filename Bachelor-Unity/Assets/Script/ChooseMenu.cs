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
    public GameObject text;
    public TMP_Text errorText;

    Toggle tgToggle;
    Toggle nnToggle;
    Toggle odToggle;

    private void Start()
    {
        //Check if we just clicked "Back to Options" from the pointcloud 
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

    //Creates a Json file from the s7k file
    public void RunVisualsWithPath()
    {
        string path = inputField.GetComponent<TMP_InputField>().text;
        print(path);
        print(File.Exists(path));
        //Check if the file path is valid/exists on the system
        if (File.Exists(path))
        {
            //Check if the file is an s7k file
            if(path.Substring(path.Length - 4) == ".s7k")
            {
                //Create the process that runs the python exe (which creates the json file)
                text.SetActive(false);
                Process p = new Process();
                p.StartInfo = new ProcessStartInfo();
                p.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                p.StartInfo.FileName = Application.streamingAssetsPath + @"/jsonload.exe";
                p.StartInfo.WorkingDirectory = Application.streamingAssetsPath;
                p.StartInfo.Arguments = path;
                p.Start();
                p.WaitForExit();

                controller.setPath(Application.streamingAssetsPath + @"/7k_data_extracted_rotated.json");

                print("ChooseMenu calls controller.LoadController");
                controller.LoadController();
                chooseMenu.SetActive(false);
                optionsMenu.SetActive(true);
                print("well done bro you can choose the right file type *clap*");
            }
            else
            {
                errorText.text = "File is not an .s7k file.";
                text.SetActive(true);
            }
        }
        else
        {
            errorText.text = "Path does not exist.";
            text.SetActive(true);
        }

        
    
    }

    //Changes the background to a new image (only used for when we switch to TemplateMenu)
    public void changeBackground()
    {
        panel.sprite = sprite;
    }

    //Quits the program
    public void quitButton()
    {
        print("QUIT PROGRAM!");
        Application.Quit();
    }

}
