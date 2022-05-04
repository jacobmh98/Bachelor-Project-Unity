using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class OptionsMenu : MonoBehaviour
{

    public Toggle toggle;
    public Toggle toggle2;
    public GameObject input1;
    public GameObject input2;
    Controller controller = Controller.getInstance();
    public TMP_Dropdown dropdown;
    
    public void toggleNN(bool newBool)
    {
        controller.nearestNeighbourEnabled = toggle.isOn;
        input1.SetActive(toggle.isOn);
        input2.SetActive(toggle.isOn);
    }

    public void toggleOD(bool newBool)
    {
        controller.outlierHeightEnabled = toggle.isOn;
        input1.SetActive(toggle.isOn);
    }

    public void toggleTriangle(bool newBool)
    {
        controller.triangulate = toggle.isOn;
        dropdown.interactable = toggle.isOn;
        if (toggle.isOn)
        {
            if(dropdown.value == 1)
            {
                toggle2.interactable = true;
            }
            else
            {
                toggle2.interactable = false;
            }
        }
        else
        {
            toggle2.interactable = false;
        }
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

    public void toggleHeightMap()
    {
        controller.heightMap = toggle.isOn;
        if(toggle.interactable == false)
        {
            controller.heightMap = false;
        }
    }



}
