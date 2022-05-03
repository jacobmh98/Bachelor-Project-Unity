using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MeshTypeControls : MonoBehaviour
{
    ToggleGroup toggleGroup;

    private void Start()
    {
        toggleGroup = GetComponent<ToggleGroup>();
        
    }
    void Update()
    {
        Toggle toggle = toggleGroup.ActiveToggles().FirstOrDefault();
        

    }
}
