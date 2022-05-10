using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class OptionsUpdate : MonoBehaviour
{

    public TMP_InputField input1;
    public TMP_InputField input2;
    public TMP_InputField input3;
    DataBase db = DataBase.getInstance();

    void Start()
    {
        input1.text = db.getNumberOfNeighbours().ToString();
        input2.text = db.getNeighbourDistance().ToString();
        input3.text = db.getOutlierHeightThreshold().ToString();
    }

}
