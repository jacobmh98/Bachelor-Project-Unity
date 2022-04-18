using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using DelaunatorSharp;
using DelaunatorSharp.Unity.Extensions;

[RequireComponent(typeof(MeshFilter))]

public class ObjData
{
    public Vector3 pos;
    public Vector3 scale;
    public Quaternion rot;

    public Matrix4x4 matrix
    {
        get
        {
            return Matrix4x4.TRS(pos, rot, scale);
        }
    }

    public ObjData(Vector3 pos, Vector3 scale, Quaternion rot)
    {
        this.pos = pos;
        this.scale = scale;
        this.rot = rot;
    }
}

public class PointCloudSpawn : MonoBehaviour
{
    public Mesh objMesh;
    public Material objMat;
    public List<Vector3> points;
    private List<List<ObjData>> batches = new List<List<ObjData>>();
    static LoadData loadData = LoadData.getInstance();

    void Start()
    {
        points = loadData.getPoints();

        createPointCloud();
    }

    private void createPointCloud()
    {
        int batchIndexNum = 0;
        List<ObjData> currBatch = new List<ObjData>();
        for (int i = 0; i < points.Count; i++)
        {
            AddObj(currBatch, i);
            batchIndexNum++;
            if (batchIndexNum >= 1000)
            {
                batches.Add(currBatch);
                currBatch = BuildNewBatch();
                batchIndexNum = 0;
            }
        }
    }

    void Update()
    {
        RenderBatches();
    }

    private void AddObj(List<ObjData> currBatch, int i)
    {
        Vector3 position = points[i];
        currBatch.Add(new ObjData(position, new Vector3(1f, 1f, 1f), Quaternion.identity));
    }

    private List<ObjData> BuildNewBatch()
    {
        return new List<ObjData>();
    }

    private void RenderBatches()
    {
        foreach (var batch in batches)
        {
            Graphics.DrawMeshInstanced(objMesh, 0, objMat, batch.Select((a) => a.matrix).ToList());
        }
    }
}
