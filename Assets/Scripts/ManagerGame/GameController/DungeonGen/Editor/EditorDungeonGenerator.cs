using UnityEditor;
using UnityEngine;

public class EditorDungeonGenerator : Editor
{
    public override void OnInspectorGUI()
    {
        AdvancedDungeonGenerator DungeonGenerator =
        (AdvancedDungeonGenerator)target;

        if (GUILayout.Button("Generate Dungeon"))
        {
            DungeonGenerator.GenerateDungeon();
        }

        DrawDefaultInspector();
    }
}