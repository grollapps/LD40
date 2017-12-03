using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelManager))]
public class LevelManagerEditor : Editor {

    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        if (GUILayout.Button("Update checkpoint list")) {
            LevelManager lm = (LevelManager)target;
            lm.UpdateCheckpointList();
        }

    }
}