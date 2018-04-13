using UnityEngine;
using System.Collections;
#if UNITY_EDITOR 
using UnityEditor;

[CustomEditor(typeof(Debug_CameraPos))]
public class CameraPos_Editor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Debug_CameraPos myscript = (Debug_CameraPos)target;
        if (GUILayout.Button("Set Position"))
        {
            myscript.ChangePos();
        }
    }
}
#endif
