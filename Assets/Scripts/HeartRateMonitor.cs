using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeartRateMonitor : MonoBehaviour
{
    public Color color;
    public Texture2D echo;
    public float resizeRatio;
    public bool changeResize;

    private int cptEcho = 0;
    public Image image;
    public float frequency;
    public float maxDisplay;
    private Texture2D texture;
    private float cptFrequencyDisplay = 0;
    private float cptFrequencyNewEntry = 0;

    // Use this for initialization
    void Start()
    {
        // Create a new 2x2 texture ARGB32 (32 bit with alpha) and no mipmaps
        texture = new Texture2D(100, 64, TextureFormat.ARGB32, false);

        // set the pixel values
        for (int i = 0; i < texture.width; ++i)
        {
            for (int j = 0; j < texture.height; ++j)
            {
                texture.SetPixel(i, j, new Color(0,0,0,0));
            }
            texture.SetPixel(i, texture.height / 2, new Color(color.r, color.g, color.b, 1));
            texture.SetPixel(i, texture.height / 2 - 1, new Color(color.r, color.g, color.b, 1));
        }

        // Apply all SetPixel calls
        texture.Apply();
        image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));

    }

    // Update is called once per frame
    void Update()
    {
        if (changeResize)
        {
            Debug.Log(Mathf.RoundToInt(echo.width * resizeRatio));
            //echo.Resize(Mathf.RoundToInt(echo.width * resizeRatio), echo.height);
            Debug.Log("RESIZE INCOMING");
            changeResize = false;
        }
          //Decalage
        if (cptFrequencyDisplay > maxDisplay)
        {
            for (int i = texture.width - 1; i > 0; --i)
            {
                for (int j = 0; j < texture.height; ++j)
                    texture.SetPixel(i, j, texture.GetPixel(i - 1, j));
            }
            cptFrequencyDisplay = 0;
            cptEcho++;

            if (cptFrequencyNewEntry > frequency)
            {
                cptFrequencyNewEntry = 0;
                cptEcho = 0;
            }
            else
            {
                cptFrequencyNewEntry += Time.deltaTime;
                for (int i = 0; i < texture.height; ++i)
                {
                    texture.SetPixel(0, i, new Color(0, 0, 0, 0));
                }
                texture.SetPixel(0, texture.height / 2, new Color(color.r, color.g, color.b, 1));
                texture.SetPixel(0, texture.height / 2 - 1, new Color(color.r, color.g, color.b, 1));
            }
        }
         
        //New entry
        if (cptEcho < echo.width)
        {
            for (int i = 0; i < texture.height; ++i)
            {
                texture.SetPixel(0, i, new Color(color.r, color.g, color.b, echo.GetPixel(echo.width - cptEcho - 1, i).a));
            }
        }

        cptFrequencyDisplay += Time.deltaTime;
        texture.Apply();
        image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
    }

}
