using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TemplateMenu : MonoBehaviour
{
    Controller controller = Controller.getInstance();

    public TMP_Dropdown dropdown;
    public Image panel;
    public Sprite sprite;

    public void loadTemplate()
    {
        if (dropdown.value == 0)
        {
            controller.setPath(Application.streamingAssetsPath + @"/NBS-Snippets-Sensor-WC+-+1_JSON.json");

        }
        else if (dropdown.value == 1)
        {
            controller.setPath(Application.streamingAssetsPath + @"/NBS-Snippets-Sensor-WC_JSON.json");
        }

        Debug.Log("Call Load controller");
        controller.LoadController();
    }

    public void changeBackground()
    {
        panel.sprite = sprite;
    }

}
