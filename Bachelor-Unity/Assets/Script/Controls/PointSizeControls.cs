using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PointSizeControls : MonoBehaviour, IPointerUpHandler
{
    Slider slider;
    Text pointSizeText;
    float prevVal = 0;
    Controller controller = Controller.getInstance();

    void Start()
    {
        slider = GetComponent<Slider>();
        pointSizeText = GameObject.Find("pointSize").GetComponent<Text>();
        
        slider.onValueChanged.AddListener(delegate { valueChangeCheck(); });
        prevVal = slider.value;
    }

    void valueChangeCheck()
    {
        pointSizeText.text = "Point Size: " + Math.Round((Decimal)slider.value, 2);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (slider.value != prevVal)
        {
            prevVal = slider.value;
            controller.updatePointSize = true;
            controller.particleSize = prevVal;

        }
    }
}
