using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OptionsMenu : MonoBehaviour
{

    public Toggle toggle;
    Controller controller = Controller.getInstance();

    public void toggleNN(bool newBool)
    {
        controller.nearestNeighbourEnabled = toggle.isOn;
    }

    public void toggleOH(bool newBool)
    {
        controller.outlierHeightEnabled = toggle.isOn;
    }



}
