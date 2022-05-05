using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointCloudControls : MonoBehaviour
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
        controller.showPointCloud = toggle.isOn;
        controller.updatePointCloud = true;
    }

    void Update()
    {
        
    }
}
