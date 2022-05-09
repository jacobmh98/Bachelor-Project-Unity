using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DelaunatorSharp;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class GenerateMesh : MonoBehaviour
{
    DataBase db = DataBase.getInstance();
    Controller controller = Controller.getInstance();
    Hashtable map;
    List<Color> colors = new List<Color>();
    Triangulate t = null;

    private void Start()
    {
        if (db.getTriangulationEnabled())
        {
            Mesh mesh = new Mesh();

            controller.mesh = mesh;

            Triangulate();

            mesh.Clear();
            mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

            mesh.vertices = controller.getPoints().ToArray();
            mesh.triangles = t.getTriangles().ToArray();
            mesh.colors = colors.ToArray();

            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            GetComponent<MeshFilter>().mesh = controller.mesh;
        }
    }

    public void Triangulate()
    {
        if (t == null)
            t = new Triangulate(controller.getPoints(), controller.getPointsDelaunay());
    }

    private void Update()
    {
        if (db.getUpdateOceanFloor() && db.getShowMesh()) {
            this.gameObject.GetComponent<Renderer>().enabled = true;
            db.setUpdateOceanFloor(false);
        } else if (db.getUpdateOceanFloor() && !db.getShowMesh()) {
            this.gameObject.GetComponent<Renderer>().enabled = false;
            db.setUpdateOceanFloor(false);
        }
    }
}