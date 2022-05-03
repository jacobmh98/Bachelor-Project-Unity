using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DelaunatorSharp;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class GenerateMesh : MonoBehaviour
{
    Controller controller = Controller.getInstance();
    Hashtable map;
    List<Color> colors = new List<Color>();
    List<int> sideLengths = new List<int>();
    int medianLength = 4;
    int removedTriangles = -1;
    int removedTriIterations = 0;
    Mesh mesh;

    private void Start()
    {
        if (controller.triangulate)
        {
            mesh = new Mesh();
            
            controller.mesh = mesh;
            controller.Triangulate();
            controller.triangulate = false;
        }
    }

    private void Update()
    {

        if (controller.showMesh && controller.mesh != null)
        {
            mesh = controller.mesh;
            
            mesh.Clear();
            mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

            mesh.vertices = controller.getPoints().ToArray();
            mesh.triangles = controller.getTriangles().ToArray();
            mesh.colors = colors.ToArray();

            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            controller.showMesh = false;
        }
    }
}