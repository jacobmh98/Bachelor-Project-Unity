using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(GameObject))]

public class GenerateHeightmap : MonoBehaviour
{
    Controller controller = Controller.getInstance();
    DataBase db = DataBase.getInstance();

    Hashtable map;

    List<Color> colors = new List<Color>();
    public Gradient gradient;
    bool hasRun = false;

    private void Update()
    {
        /*
         * Repeatedly check if heightmap is enabled once per second
         */
        if (db.getTriangulationEnabled() && !hasRun && db.getPoints().Count > 2)
        {
            
            if (colors.Count == 0)
            {
                int finalShallowDepth = db.getNewShallowDepth();
                int finalDeepDepth = db.getNewDeepDepth();

                // Define the colors of each vertex evaluated in the gradient
                foreach (Vector3 p in db.getPoints())
                {
                    float height = Mathf.InverseLerp(finalDeepDepth, finalShallowDepth, p[1]);
                    colors.Add(gradient.Evaluate(height));
                }

            }
             // Create the mesh and set it to component
            Mesh mesh = controller.mesh;
            mesh.colors = colors.ToArray();
            GetComponent<MeshFilter>().mesh = controller.mesh;
            hasRun = true;
        }
        else if (db.getUpdateHeightMap() && db.getShowHeightMap())
        {
            this.gameObject.GetComponent<Renderer>().enabled = true;

        } else if(db.getTriangulationEnabled() && !db.getShowHeightMap())
        {
            this.gameObject.GetComponent<Renderer>().enabled = false;
        }

    }

}