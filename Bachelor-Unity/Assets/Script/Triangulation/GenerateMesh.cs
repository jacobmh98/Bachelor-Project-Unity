using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        /*
         * Initialize the triangulation algorithm and mesh
         */ 
        if (db.getTriangulationEnabled() && db.getPoints().Count > 2)
        {
            

            if (t == null)
            {
                t = new Triangulate(db.getPoints(), db.getPointsDelauney());
            }

            // Create the mesh object and set the vertices, triangles and set the game object in Unity to this mesh
            Mesh mesh = new Mesh();
            controller.mesh = mesh;
            mesh.Clear();

            // Changeing the default index format to 32 bit to allow more vertices and triangles
            mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

            mesh.vertices = db.getPoints().ToArray();
            mesh.triangles = db.getTriangles().ToArray();
            mesh.colors = colors.ToArray();

            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            GetComponent<MeshFilter>().mesh = controller.mesh;
        }

    }

    private void Update()
    {
        // Display either ocean floor material or heightmap material
        if (db.getUpdateOceanFloor() && db.getShowMesh()) {
            this.gameObject.GetComponent<Renderer>().enabled = true;
            db.setUpdateOceanFloor(false);

        } else if (db.getUpdateOceanFloor() && !db.getShowMesh()) {
            this.gameObject.GetComponent<Renderer>().enabled = false;
            db.setUpdateOceanFloor(false);
        }

    }

}