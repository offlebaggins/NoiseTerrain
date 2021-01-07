using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace CubeWorld
{

    public class GridMap : MonoBehaviour
    {
        public GameObject tilePrefab;
        
        [Header("Grid Size Settings")]
        public int gridSize = 10;

        float tileSize;

        public List<Tile> tiles = new List<Tile>();


        [Header("Noise Settings")]
        public Vector2 noiseSampleOrigin;
        public float noiseOffsetSpeed;
        public TerrainSetting terrain;

        public float colorUpdateFrequency = 0.1f;

        private void OnValidate()
        {
            //UpdateTiles();
        }

        private void FixedUpdate()
        {
            noiseSampleOrigin += new Vector2(Input.GetAxis("Horizontal") * noiseOffsetSpeed, Input.GetAxis("Vertical") * noiseOffsetSpeed);
        }

        public void Generate()
        {
            foreach (Tile tile in tiles)
            {
                if (Application.isEditor)
                {
                    DestroyImmediate(tile.gameObject);
                }
                else
                {
                    Destroy(tile.gameObject);
                }
            }
            tiles = new List<Tile>();

            tileSize = 1;// tilePrefab.GetComponent<Collider>().bounds.size.x;

            for (int x = 0; x < gridSize; x++)
            {
                for (int z = 0; z < gridSize; z++)
                {
                    if (Vector3.Distance(new Vector3(x, 0, z), new Vector3(gridSize/2, 0, gridSize/2)) < gridSize / 2)
                    {
                        GameObject newTile = Instantiate(tilePrefab).gameObject;

                        newTile.name = "Tile (" + x + ", " + z + ")";
                        newTile.transform.parent = transform;
                        Tile tile = newTile.GetComponent<Tile>();
                        tile.position = new Vector2(x, z);
                        tiles.Add(tile);

                        newTile.transform.position = transform.position + new Vector3(x * tileSize, 0, z * tileSize);
                        tile.UpdateTargetHeight(noiseSampleOrigin, terrain);
                        tile.SetColor(terrain);
                    }
                }
            }
        }

        void UpdateTiles()
        {
            foreach (Tile tile in tiles)
            {
                tile.UpdateTargetHeight(noiseSampleOrigin, terrain);
            }
        }

        public void UpdateTileColors()
        {
            foreach (Tile tile in tiles)
            {
                tile.SetColor(terrain);
            }
        }
    }
}
