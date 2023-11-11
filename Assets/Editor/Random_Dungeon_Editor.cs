using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Abstract_Dungeon_Generator), true)]
public class Random_Dungeon_Editor : Editor
{
    Abstract_Dungeon_Generator generator;

    private void Awake()
    {
        generator = (Abstract_Dungeon_Generator) target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Create Dungeon"))
        {
            generator.GenerateDungeon();
        }
    }
}
