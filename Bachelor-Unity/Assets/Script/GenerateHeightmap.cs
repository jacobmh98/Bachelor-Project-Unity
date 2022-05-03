using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DelaunatorSharp;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class GenerateHeightmap : MonoBehaviour
{
    Controller controller = Controller.getInstance();
    Hashtable map;
    List<Color> colors = new List<Color>();
    public Gradient gradient;
    

    private void Update()
    {
        if (controller.triangulate && controller.showHeightmap && !controller.showMesh && controller.mesh != null)
        {
            Mesh mesh = controller.mesh;
            if (colors.Count == 0) { 
                // Define the colors of mesh
                foreach (Vector3 p in controller.getPoints())
                {
                    float height = Mathf.InverseLerp(controller.getMaxDepth(), controller.getMinDepth(), p[1]);
                    colors.Add(gradient.Evaluate(height));
                    
                }
            }

            mesh.colors = colors.ToArray();

            GetComponent<MeshFilter>().mesh = controller.mesh;
            controller.triangulate = false;
        }
    }
}