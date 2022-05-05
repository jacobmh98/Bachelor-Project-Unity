using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TemplateMenu : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public string templateName;
    public int templateChosen = -1;

    public Image panel;
    public Sprite sprite;

    public void loadTemplate()
    {
        if (dropdown.value == 0)
        {
            templateName = "NBS-Snippets-Sensor-WC+-+1.s7k";
            templateChosen = dropdown.value;

        } else if (dropdown.value == 1)
        {
            templateName = "NBS-Snippets-Sensor-WC.s7k";
            templateChosen = dropdown.value;
        }
    }
    public void changeBackground()
    {
        panel.sprite = sprite;
    }
}
