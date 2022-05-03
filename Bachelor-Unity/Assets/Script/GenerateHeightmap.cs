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
    Mesh mesh;

    private void Update()
    {
        if (controller.showHeightmap)
        {
            mesh = controller.mesh;
            GetComponent<MeshFilter>().mesh = mesh;

            if(colors.Count == 0) { 
                // Define the colors of mesh
                foreach (Vector3 p in controller.getPoints())
                {
                    float height = Mathf.InverseLerp(controller.getMaxDepth(), controller.getMinDepth(), p[1]);
                    colors.Add(gradient.Evaluate(height));
                }
            }

            mesh.Clear();

            mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

            mesh.vertices = controller.getPoints().ToArray();
            mesh.triangles = controller.getTriangles().ToArray();
            mesh.colors = colors.ToArray();

            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            controller.showHeightmap = false;
        }
    }
}