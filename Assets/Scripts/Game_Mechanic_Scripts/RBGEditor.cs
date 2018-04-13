using UnityEngine;
using System.Collections;
#if UNITY_EDITOR 
using UnityEditor;
[CustomEditor(typeof(RoomBoundsGenerator))]
public class RBGEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        RoomBoundsGenerator myscript = (RoomBoundsGenerator)target;
        if (GUILayout.Button("Generate Bounds"))
        {
            myscript.makeRoom();
        }
    }
}
#endif
