using UnityEngine;
using UnityEngine.UI;

public class PointCloudControls : MonoBehaviour
{
    DataBase db = DataBase.getInstance();
    
    public GameObject scale;
    Toggle displayToggle;
    public Toggle toggleGradient;
    public Toggle toggleHeightmap;


    void Start()
        /* Finding specific display toggle
         */
    {
        displayToggle = GetComponent<Toggle>();
        displayToggle.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    private void ValueChangeCheck()
        /* Sets corresponding values in database
         */
    {
        db.setShowPointCloud(displayToggle.isOn);
        db.setUpdatePointCloud(true);

        if(!displayToggle.isOn)
        {
            toggleGradient.interactable = false;
        } else
        {
            toggleGradient.interactable = true;
        }

    }

}
