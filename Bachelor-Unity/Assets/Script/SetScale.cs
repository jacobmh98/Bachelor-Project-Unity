using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class SetScale : MonoBehaviour
{
    DataBase db = DataBase.getInstance();

    public TMP_Text textShallow;
    public TMP_Text textHalf;
    public TMP_Text textDeep;
    public GameObject scale;
    public Toggle toggleHeightMap;
    public Toggle toggleGradient;

    // Start is called before the first frame update
    void Start()
    {
        textShallow.text = Math.Abs(db.getSliderValueShallowDepth()).ToString();
        textDeep.text = Math.Abs(db.getSliderValueDeepDepth()).ToString();
        textHalf.text = (Math.Abs(db.getSliderValueShallowDepth() + db.getSliderValueDeepDepth()) / 2).ToString();
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
