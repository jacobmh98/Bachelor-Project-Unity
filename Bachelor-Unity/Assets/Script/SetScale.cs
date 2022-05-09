using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetScale : MonoBehaviour
{

    public TMP_Text textShallow;
    public TMP_Text textDeep;
    Controller cont = Controller.getInstance();

    // Start is called before the first frame update
    void Start()
    {
        textShallow.text = cont.getMinDepth().ToString();
        textDeep.text = cont.getMaxDepth().ToString();

    }

}
