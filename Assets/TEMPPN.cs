using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TEMPPN : MonoBehaviour
{
    public int width = 256;
    public int length = 256;
    public float sample;
    public float scale = 20f;

    public Image image;

    public Transform player;
    private void Update()
    {
        image.material.mainTexture = GenerateTexture();
    }

    Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(width, length);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                Color color = CalculateColor(x, y);
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();
        return texture;
    }

    Color CalculateColor(int x, int y)
    {
        float xCoord = ((float)x / width * scale) * player.position.x;
        float yCoord = ((float)y / length * scale) * player.position.z;

        sample = Mathf.PerlinNoise(xCoord, yCoord);
        return new Color(sample, sample, sample);
    }
}
