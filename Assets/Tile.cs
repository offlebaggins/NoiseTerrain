using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CubeWorld
{
    

    public class Tile : MonoBehaviour
    {
        public Vector2 position;
        GridMap grid;

        MeshRenderer meshRenderer;

        float targetY, offsetY;

        bool isWater = false;

        private void Awake()
        {
            grid = FindObjectOfType<GridMap>();
            meshRenderer = GetComponent<MeshRenderer>();

            StartCoroutine(UpdateColor());
        }

        private void FixedUpdate()
        {
            UpdateTargetHeight(grid.noiseSampleOrigin, grid.terrain);
            float y = targetY;
            if (isWater)
            {
                offsetY = grid.terrain.GetWaveOffset(position);

                y = targetY - offsetY;
                if (y <= 1) y = 1f;
            }

            transform.localScale = new Vector3(1, y, 1);
            transform.position = new Vector3(transform.position.x, y / 2, transform.position.z);
            //SetColor(grid.terrain);
        }

        public void UpdateTargetHeight(Vector2 noiseSampleOrigin, TerrainSetting terrain)
        {
            if (grid == null) grid = FindObjectOfType<GridMap>();
            if (meshRenderer == null) meshRenderer = FindObjectOfType<MeshRenderer>();
            float y = grid.terrain.GetTerrainNoiseValue(position, noiseSampleOrigin);

            if (y <= 1) y = 1f;

            if (y < terrain.seaLevel)
            {
                y = terrain.seaLevel;
                isWater = true;
            }
            else
            {
                //meshRenderer.material = terrain.defaultMat;
                
                isWater = false;
            }

            targetY = y;

            
        }

        public void SetColor(TerrainSetting terrain)
        {
            if (Application.isPlaying)
            {
                meshRenderer.material.color = GetTargetColor(terrain);
            }
        }

        Color GetTargetColor(TerrainSetting terrain)
        {
            if (targetY <= terrain.seaLevel)
            {
                return terrain.colorGradient.Evaluate(0);
            }
            else
            {
                return terrain.colorGradient.Evaluate(targetY / terrain.maxTerrainHeight);
            }
        }

        IEnumerator UpdateColor()
        {
            while(true)
            {
                yield return new WaitForSeconds(grid.colorUpdateFrequency);
                SetColor(grid.terrain);
            }
        }
    }
}
