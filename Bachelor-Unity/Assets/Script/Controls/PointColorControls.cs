using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointColorControls : MonoBehaviour
{
    Toggle toggle;
    public GameObject scale;
    public Toggle toggleHeightMap;
    Controller controller = Controller.getInstance();

    void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(delegate { valueChangeCheck(); });
        scale.SetActive(toggle.isOn);
    }

    private void valueChangeCheck()
    {
        controller.pointCloudGradient = toggle.isOn;
        controller.updatePointColor = true;
        if (toggleHeightMap.isOn)
        {
            scale.SetActive(true);
        }
        else
        {
            scale.SetActive(toggle.isOn);
        }

    }

    void Update()
    {

    }
}
