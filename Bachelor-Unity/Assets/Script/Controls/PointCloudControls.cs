using UnityEngine;
using UnityEngine.UI;

public class PointCloudControls : MonoBehaviour
{
    DataBase db = DataBase.getInstance();
    Toggle toggle;

    void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    private void ValueChangeCheck()
    {
        db.setShowPointCloud(toggle.isOn);
        db.setUpdatePointCloud(true);
    }

}
