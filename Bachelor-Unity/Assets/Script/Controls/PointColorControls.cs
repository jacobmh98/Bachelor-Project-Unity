using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointColorControls : MonoBehaviour
{
    Toggle toggle;
    Controller controller = Controller.getInstance();

    void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(delegate { valueChangeCheck(); });
    }

    private void valueChangeCheck()
    {
        controller.pointCloudGradient = toggle.isOn;
        controller.updatePointColor = true;
    }

    void Update()
    {

    }
}