using UnityEngine;
using UnityEngine.UI;

public class BoatPathControls : MonoBehaviour
{

    DataBase db = DataBase.getInstance();
    Toggle toggle;

    // Start is called before the first frame update
    void Start()
    {
        /*Finding specific boat path toggle
         */
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    private void ValueChangeCheck()
    {
        /*Changing values in database
        */
        db.setShowBoatPathPoints(toggle.isOn);
        db.setUpdateBoatPath(true);
    }

}
