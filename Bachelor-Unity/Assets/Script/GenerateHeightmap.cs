using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DelaunatorSharp;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(GameObject))]

public class GenerateHeightmap : MonoBehaviour
{
    Controller controller = Controller.getInstance();
    Hashtable map;
    List<Color> colors = new List<Color>();
    public Gradient gradient;
    bool hasRun = false;

    private void Start()
    {
        //print(this.gameObject.)
    }

    private void Update()
    {
        if(controller.triangulate && !hasRun)
        {
            Mesh mesh = controller.mesh;
            if (colors.Count == 0)
            {
                // Define the colors of mesh
                foreach (Vector3 p in controller.getPoints())
                {
                    float height = Mathf.InverseLerp(controller.getMaxDepth(), controller.getMinDepth(), p[1]);
                    colors.Add(gradient.Evaluate(height));

                }
            }

            mesh.colors = colors.ToArray();

            GetComponent<MeshFilter>().mesh = controller.mesh;
            hasRun = true;
        }
        else if (controller.updateHeightmap && controller.showHeightmap)
        {
            this.gameObject.GetComponent<Renderer>().enabled = true;
        } else if(controller.triangulate && !controller.showHeightmap)
        {
            this.gameObject.GetComponent<Renderer>().enabled = false;
        }
    }
}