using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class OptionsMenu : MonoBehaviour
{
    DataBase db = DataBase.getInstance();

    public Toggle toggle;
    public Toggle toggle2;
    public TMP_InputField input1;
    public TMP_InputField input2;
    public GameObject text1;
    public GameObject text2;

    public TMP_Dropdown dropdown;
    
    public void toggleNN(bool newBool)
    {
        db.setNearestNeighbourEnabled(toggle.isOn);
        input1.interactable = toggle.isOn;
        input2.interactable = toggle.isOn;
        //text1.SetActive(toggle.isOn);
        //text2.SetActive(toggle.isOn);
    }

    public void toggleOD(bool newBool)
    {
        db.setOutlierHeightEnabled(toggle.isOn);
        input1.interactable = (toggle.isOn);
        //text1.SetActive(toggle.isOn);
    }

    public void toggleTriangle(bool newBool)
    {
        db.setTriangulationEnabled(toggle.isOn);
        dropdown.interactable = toggle.isOn;
    }

}
