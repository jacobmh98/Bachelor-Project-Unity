using UnityEngine;
using UnityEngine.UI;

public class PointColorControls : MonoBehaviour
{
    DataBase db = DataBase.getInstance();

    Toggle togglePointHeightmap;
    public GameObject scale;
    public Toggle toggleHeightMap;

    void Start()
        /* Finding specific point height map toggle, and setting scale to toggle's state
         */
    {
        togglePointHeightmap = GetComponent<Toggle>();
        togglePointHeightmap.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        scale.SetActive(togglePointHeightmap.isOn);
    }

    private void ValueChangeCheck()
        /* Setting corresponding values in database
         */
    {
        db.setPointCloudGradient(togglePointHeightmap.isOn);
        db.setUpdatePointColor(true);
    }

    
    void Update()
    // Checking toggles for the appearence of the height scale.
    {
        if (db.getShowHeightMap())
        {
            scale.SetActive(true);
        }
        else if (db.getShowPointCloud())
        {
            if (db.getPointCloudGradient())
            {
                scale.SetActive(true);
            }
            else
            {
                scale.SetActive(false);
            }

        }
        else
        {
            scale.SetActive(false);
        }

    }

}
