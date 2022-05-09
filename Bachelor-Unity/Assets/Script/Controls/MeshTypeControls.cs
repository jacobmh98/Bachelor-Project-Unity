using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MeshTypeControls : MonoBehaviour
{
    ToggleGroup toggleGroup;
    Controller controller = Controller.getInstance();

    private void Start()
    {
        toggleGroup = GetComponent<ToggleGroup>();
        
    }
    void Update()
    {
        Toggle toggle = toggleGroup.ActiveToggles().FirstOrDefault();

        var gameObject = GameObject.Find("MeshType");

        if (!controller.triangulate)
            gameObject.SetActive(false);

        if(Equals(toggle.name, "oceanfloor"))
        {
            controller.showHeightmap = false;
            controller.showMesh = true;

            controller.updateHeightmap = true;
            controller.updateOceanfloor = true;
        } else if(Equals(toggle.name, "heightmap"))
        {
            controller.showHeightmap = true;
            controller.showMesh = false;

            controller.updateHeightmap = true;
            controller.updateOceanfloor = true;
        } else
        {
            controller.showHeightmap = false;
            controller.showMesh = false;

            controller.updateHeightmap = true;
            controller.updateOceanfloor = true;
        }
    }
}
