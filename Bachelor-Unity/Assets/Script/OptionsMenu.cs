using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OptionsMenu : MonoBehaviour
{

    public Toggle toggle;
    Controller controller = Controller.getInstance();

    public void toggleDepth(bool newBool)
    {
        controller.depthFilter = toggle.isOn;
        print(toggle.isOn);
    }

    public void toggleAxis(bool newBool)
    {
        controller.axisFilter = toggle.isOn;
    }

    public void toggleNN(bool newBool)
    {
        controller.nearestNeighbour = toggle.isOn;
    }

    public void toggleOH(bool newBool)
    {
        controller.outlierHeight = toggle.isOn;
    }



}
