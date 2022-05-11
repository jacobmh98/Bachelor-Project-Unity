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

    public void loadTemplate()
    {
        if (dropdown.value == 0)
        {
            controller.setPath(Application.streamingAssetsPath + @"/NBS-Snippets-Sensor-WC_JSON.json");
            db.setNumberOfNeighbours(30);
            db.setNeighbourDistance(1);
            db.setOutlierHeightThreshold(1);
        }
        else if (dropdown.value == 1)
        {
            controller.setPath(Application.streamingAssetsPath + @"/NBS-Snippets-Sensor-WC+-+1_JSON.json");
            db.setNumberOfNeighbours(50);
            db.setNeighbourDistance(0.9);
            db.setOutlierHeightThreshold(19);
        }

        controller.LoadController();

    }

    public void changeBackground()
    {
        panel.sprite = sprite;
    }

}
