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
    Controller cont = Controller.getInstance();

    public void RunVisualsWithPath()
    {
        StateNameController.SevenKPath = inputField.GetComponent<TMP_InputField>().text;
        string path = StateNameController.SevenKPath;
        print(StateNameController.SevenKPath);
        Process p = new Process();
        p.StartInfo = new ProcessStartInfo();
        p.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
        p.StartInfo.FileName = Application.streamingAssetsPath + @"/cartesian_coordinates.exe";
        p.StartInfo.Arguments = path;
        p.Start();
        p.WaitForExit();
        var parentPath = Directory.GetParent(Application.dataPath);
        
        cont.setPath(Application.dataPath + "/../7k_data_extracted.json");
        cont.LoadController();
    
    }


}
