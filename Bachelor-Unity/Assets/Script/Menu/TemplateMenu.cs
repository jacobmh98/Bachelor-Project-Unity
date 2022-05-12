using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TemplateMenu : MonoBehaviour
{
    DataBase db = DataBase.getInstance();
    Controller controller = Controller.getInstance();

    public TMP_Dropdown dropdown;
    public Image panel;
    public Sprite sprite;
    public TMP_InputField input1;
    public TMP_InputField input2;
    public TMP_InputField input3;

    public void loadTemplate()
    {
        print("loading template");
        if (dropdown.value == 0)
        {
            print("loading default values for 0");
            controller.setPath(Application.streamingAssetsPath + @"/NBS-Snippets-Sensor-WC_JSON.json");
            db.setNumberOfNeighbours(30);
            db.setNeighbourDistance(1);
            db.setOutlierHeightThreshold(1);
            input1.text = db.getNumberOfNeighbours().ToString();
            input2.text = db.getNeighbourDistance().ToString();
            input3.text = db.getOutlierHeightThreshold().ToString();

        }
        else if (dropdown.value == 1)
        {
            print("loading default values for 1");
            controller.setPath(Application.streamingAssetsPath + @"/NBS-Snippets-Sensor-WC+-+1_JSON.json");
            db.setNumberOfNeighbours(50);
            db.setNeighbourDistance(0.9);
            db.setOutlierHeightThreshold(19);
            input1.text = db.getNumberOfNeighbours().ToString();
            input2.text = db.getNeighbourDistance().ToString();
            input3.text = db.getOutlierHeightThreshold().ToString();
        }

        controller.LoadController();

    }

    public void changeBackground()
    {
        panel.sprite = sprite;
    }

}
