using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class OptionsMenu : MonoBehaviour
{
    public static bool depthFilter = false;
    public static bool axisFilter = false;
    public static bool nearestNeighbour = false;
    public static bool outlierHeight = false;
    public static int minDepth = 0;
    public static int maxDepth = 10;

    public Toggle toggle;
    public GameObject inputNeighbours;
    public GameObject inputDistance;
    Controller controller = Controller.getInstance();
    public TMP_Dropdown dropdown;

    public void toggleNN(bool newBool)
    {
        controller.nearestNeighbour = toggle.isOn;
        inputNeighbours.SetActive(toggle.isOn);
        inputDistance.SetActive(toggle.isOn);
    }

    public void toggleOD(bool newBool)
    {
        controller.z_score_outlier_detection = toggle.isOn;
    }

    public void toggleTriangle(bool newBool)
    {
        controller.triangulate = toggle.isOn;
        dropdown.interactable = toggle.isOn;
    }

    public void dropDownDelaunay()
    {
        if(dropdown.value == 1)
        {
            toggle.interactable = true;
        } else
        {
            toggle.interactable = false;
        }
    }

    

    

    
}
