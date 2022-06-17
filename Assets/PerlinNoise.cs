using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerlinNoise : MonoBehaviour
{
    public Texture2D Texture2D;

    static private int XSize = 100, YSize = 100;

    public float Zoom = 30f;

    public float offset;
    public Material BrownMaterial;
public GameObject BlockPref;
    
    private MeshRenderer[,] GameObjects2d = new MeshRenderer[XSize, YSize];
    
    public float relief = 10;

    // Start is called before the first frame update
    void Start()
    {
        Texture2D = new Texture2D(XSize, YSize);
        for (int x = 0; x < XSize; x++)
        {
            for (int y = 0; y < YSize; y++)
            {
                GameObjects2d[x, y] = Instantiate(BlockPref.GetComponent<MeshRenderer>());
                GameObjects2d[x, y].GetComponent<MeshRenderer>();
            }
        }
        GenerateTexure();
        GenerateMap(Texture2D);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(1 / Time.deltaTime);
        offset += 8 * Time.deltaTime;
        GenerateTexure();
        GenerateMap(Texture2D);
        
    }

    void GenerateTexure()
    {
        for (int x = 0; x < XSize; x++)
        {
            for (int y = 0; y < YSize; y++)
            {
                float Clr = Mathf.PerlinNoise((x + offset) / Zoom, (y + offset) / Zoom);
                Color PixelColor = new Color(Clr, Clr, Clr, 1);
                Texture2D.SetPixel(x, y, PixelColor);
            }
        }

        Texture2D.Apply();
    }
    void GenerateMap(Texture2D texture2D)
    {
        float MaxHigh = 1 * relief;
        for (int x = 0; x < XSize; x++)
        {
            for (int y = 0; y < YSize; y++)
            {
                Color GotPixel = texture2D.GetPixel(x, y);
                float High = Mathf.RoundToInt(GotPixel.r * relief);
                float ProcentHigh = High / MaxHigh;
                if (ProcentHigh > 0.65f)
                {
                    GameObjects2d[x, y].material = BrownMaterial;
                }
                else if (ProcentHigh  <= 0.65f && ProcentHigh > 0.5f)
                {
                    GameObjects2d[x,y].material.color = Color.green;
                }
                else if(ProcentHigh <= 0.5f && ProcentHigh > 0.38f)
                {
                    GameObjects2d[x,y].material.color = Color.white;
                }
                else if (ProcentHigh <= 0.38 && ProcentHigh > 0.20f)
                {
                    GameObjects2d[x,y].material.color = Color.yellow;
                }
                else if (ProcentHigh <= 0.20f)
                {
                    GameObjects2d[x,y].material.color = Color.blue;
                }
                GameObjects2d[x, y].transform.position = new Vector3(x, High, y);
            }
        }
    }
}
