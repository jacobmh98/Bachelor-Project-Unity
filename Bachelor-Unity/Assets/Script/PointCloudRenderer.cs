using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PointCloudRenderer : MonoBehaviour
{
    DataBase db = DataBase.getInstance();
    Controller controller = Controller.getInstance();

    Texture2D texColor;
    Texture2D texPosScale;
    VisualEffect vfx;
    uint resolution = 2048;

    public float particleSize;
    public int update = 0;
    bool toUpdate = false;
    uint particleCount = 0;
    
    Vector3[] positions;
    Color[] colors;


    private void Start()
    {
        
        vfx = GetComponent<VisualEffect>();

        positions = controller.getPoints().ToArray();
        colors = new Color[positions.Length];

        for (int x = 0; x < positions.Length; x++)
        {
            colors[x] = new Color(0, 0, 0);
        }

        SetParticles(positions, colors);
    }

    private void Update()
    {
        if(db.getUpdatePointSize())
        {
            SetParticles(positions, colors);
            db.setUpdatePointSize(false);
        }
        if (toUpdate)
        {
            toUpdate = false;

            vfx.Reinit();
            vfx.SetUInt(Shader.PropertyToID("ParticleCount"), particleCount);
            vfx.SetTexture(Shader.PropertyToID("TexColor"), texColor);
            vfx.SetTexture(Shader.PropertyToID("TexPosScale"), texPosScale);
            vfx.SetUInt(Shader.PropertyToID("Resolution"), resolution);
        }

        if (db.getUpdatePointCloud() && db.getShowPointCloud())
        {
            this.gameObject.GetComponent<Renderer>().enabled = true;
            db.setUpdatePointCloud(false);
        }
        else if (db.getUpdatePointCloud() && !db.getShowPointCloud()) {
            this.gameObject.GetComponent<Renderer>().enabled = false;
            db.setUpdatePointCloud(false);
        }

    }

    public void SetParticles(Vector3[] positions, Color[] colors)
    {
        particleSize = db.getParticleSize();
        texColor = new Texture2D(positions.Length > (int)resolution ? (int)resolution : positions.Length, Mathf.Clamp(positions.Length / (int)resolution, 1, (int)resolution), TextureFormat.RGBAFloat, false);
        texPosScale = new Texture2D(positions.Length > (int)resolution ? (int)resolution : positions.Length, Mathf.Clamp(positions.Length / (int)resolution, 1, (int)resolution), TextureFormat.RGBAFloat, false);
        int texWidth = texColor.width;
        int texHeight = texColor.height;

        for (int y = 0; y < texHeight; y++)
        {
            for (int x = 0; x < texWidth; x++)
            {
                int index = x + y * texWidth;
                texColor.SetPixel(x, y, colors[index]);
                var data = new Color(positions[index].x, positions[index].y, positions[index].z, particleSize);
                texPosScale.SetPixel(x, y, data);
            }
        }

        texColor.Apply();
        texPosScale.Apply();
        particleCount = (uint)positions.Length;
        toUpdate = true;
    }
}