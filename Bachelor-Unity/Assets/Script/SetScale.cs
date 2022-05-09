using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SetScale : MonoBehaviour
{
    DataBase db = DataBase.getInstance();

    public TMP_Text textShallow;
    public TMP_Text textDeep;
    public GameObject scale;
    public Toggle toggleHeightMap;
    public Toggle toggleGradient;

    // Start is called before the first frame update
    void Start()
    {
        textShallow.text = db.getNewShallowDepth().ToString();
        textDeep.text = db.getNewDeepDepth().ToString();
        scale.SetActive(toggleHeightMap.isOn);
    }

    public void scaleAppear()
    {
        if (toggleGradient.isOn)
        {
            scale.SetActive(true);
        } else
        {
            scale.SetActive(toggleHeightMap.isOn);
        }
    }


}
