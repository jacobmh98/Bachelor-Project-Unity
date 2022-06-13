using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PointSizeControls : MonoBehaviour, IPointerUpHandler
{
    DataBase db = DataBase.getInstance();
    Slider slider;
    Text pointSizeText;
    float prevVal = 0;

    void Start()
        /* Finding specific point size slider 
         */
    {
        slider = GetComponent<Slider>();
        pointSizeText = GameObject.Find("pointSize").GetComponent<Text>();

        slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        prevVal = slider.value;
    }

    void ValueChangeCheck()
        /* Changes text to slider value
         */
    {
        pointSizeText.text = "Point Size: " + Math.Round((Decimal)slider.value, 2);
    }

    public void OnPointerUp(PointerEventData eventData)
        /* Only updates the point sizes when user lets go of handle
         */
    {
        if (slider.value != prevVal)
        {
            prevVal = slider.value;
            db.setUpdatePointSize(true);
            db.setParticleSize(prevVal);

        }

    }

}
