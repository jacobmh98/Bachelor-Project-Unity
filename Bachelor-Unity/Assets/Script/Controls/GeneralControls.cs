using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class GeneralControls : MonoBehaviour
{
    Canvas canvas;
    bool display = true;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("h"))
        {
            display = !display;
            canvas.enabled = display;
        }
            

    }
}
