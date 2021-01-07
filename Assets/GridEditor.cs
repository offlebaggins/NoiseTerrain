using UnityEngine;
using System.Collections;
using UnityEditor;
namespace CubeWorld
{
    [CustomEditor(typeof(GridMap))]
    public class GridEditor : Editor
    {
        public override void OnInspectorGUI()
        {

            GridMap grid = (GridMap)target;

            base.OnInspectorGUI();

            if (GUILayout.Button("Generate")) grid.Generate();
            if (GUILayout.Button("Update Colors")) grid.UpdateTileColors();
        }
    }
}
