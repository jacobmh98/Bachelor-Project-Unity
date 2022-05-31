using UnityEngine;
using UnityEngine.VFX;

public class BoatPathRenderer : MonoBehaviour
{
    DataBase db = DataBase.getInstance();

    Texture2D texColor;
    Texture2D texPosScale;
    VisualEffect vfx;
    uint resolution = 2048;
    public Gradient gradient;

    public float particleSize = 0.05f;
    bool toUpdate = false;
    uint particleCount = 0;

    Vector3[] positions;
    Color[] colors;
    Color[] colorsGradient;

    private void Start()
    {
        this.gameObject.GetComponent<Renderer>().enabled = false;
        int finalShallowDepth = db.getNewShallowDepth();
        int finalDeepDepth = db.getNewDeepDepth();

        vfx = GetComponent<VisualEffect>();

        positions = db.getBoatPathPoints().ToArray();
        colors = new Color[positions.Length];
        //colorsGradient = new Color[positions.Length];

        for (int x = 0; x < positions.Length; x++)
        {
            //float height = Mathf.InverseLerp(finalDeepDepth, finalShallowDepth, positions[x].y);
            //colorsGradient[x] = gradient.Evaluate(height);
            colors[x] = new Color(0.8f, 0.8f, 0.8f);
        }
        
        SetParticles(positions, colors);
    }

    private void Update()
    {
        if (toUpdate)
        {
            toUpdate = false;

            vfx.Reinit();
            vfx.SetUInt(Shader.PropertyToID("ParticleCount"), particleCount);
            vfx.SetTexture(Shader.PropertyToID("TexColor"), texColor);
            vfx.SetTexture(Shader.PropertyToID("TexPosScale"), texPosScale);
            vfx.SetUInt(Shader.PropertyToID("Resolution"), resolution);
        }
        if (db.getUpdateBoatPath() && db.getShowBoatPathPoints())
        {
            this.gameObject.GetComponent<Renderer>().enabled = true;
            db.setUpdateBoatPath(false);
        }
        else if (db.getUpdateBoatPath() && !db.getShowBoatPathPoints())
        {
            this.gameObject.GetComponent<Renderer>().enabled = false;
            db.setUpdateBoatPath(false);
        }

    }

    public void SetParticles(Vector3[] positions, Color[] colors)
    {
        particleSize = 0.1f;
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
