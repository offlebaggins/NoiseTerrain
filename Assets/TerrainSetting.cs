using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Terrain Setting")]
public class TerrainSetting : ScriptableObject
{
    [Header("Color Settings")]
    
    public Gradient colorGradient;
    public float maxTerrainHeight = 10;

    [Header("Water Settings")]
    public float seaLevel = 0;
    public float waveSpeed = 1;
    public float waveFrequency = 1;
    public float waveAmplitude;


    [Header("Terrain Settings")]
    public NoiseLayer[] noiseLayers = new NoiseLayer[1];

    public float GetTerrainNoiseValue(Vector2 position, Vector2 noiseSampleOrigin)
    {
        float n = 0;
        float weightSum = 0;
        foreach (NoiseLayer noiseLayer in noiseLayers)
        {
            n += noiseLayer.GetNoiseValue(position, noiseSampleOrigin);
            weightSum += noiseLayer.blendWeight;
        }
        return n / weightSum;
    }

    public float GetWaveOffset(Vector2 position)
    {
        float offset = position.x + position.y;
        return waveAmplitude * Mathf.Sin(Time.time * waveSpeed + offset * waveFrequency);
    }
}
