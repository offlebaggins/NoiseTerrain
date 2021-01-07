using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoiseLayer
{
    [Range(0, 0.5f)]
    public float noiseScale = 0.01f;
    public float noiseHeight = 1;
    public float blendWeight = 1;
    public float GetNoiseValue(Vector2 position, Vector2 noiseSampleOrigin)
    {
        return blendWeight * noiseHeight * Mathf.PerlinNoise((noiseSampleOrigin.x + (position.x * noiseScale)), (noiseSampleOrigin.y + (position.y * noiseScale)));
    }
}
