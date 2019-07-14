using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(TimeObject))]
public class TimeObjectEditor : Editor
{

    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();

        TimeObject to = (TimeObject)target;

        GUILayout.Label(string.Format("{0}", to.CurrentTimeState ==  TimeState.RECORDING ? "RECORDING" : "NOT RECORDING"));

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("START RECORDING"))
        {

            to.StartRecording();

        }
        
        if (GUILayout.Button("END RECORDING"))
        {

            to.EndRecording();

        }

        if (GUILayout.Button("CLEAR RECORDING"))
        {

            to.ClearTimeHistory();

        }

        if (GUILayout.Button("SAVE RECORDING"))
        {

            TimeObject.SaveSerializedTimeObjectDelta(to);

        }

        if (GUILayout.Button("LOAD RECORDING"))
        {

            to.LoadSerializedTimeObjectDelta();

        }

        EditorGUILayout.EndHorizontal();

        //

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("EDITOR REWIND"))
        {

            to.ReverseTime(false);

        }

        if (GUILayout.Button("EDITOR FORWARD"))
        {

            to.ForwardTime();

        }

        if (GUILayout.Button("EDITOR FREEZE"))
        {

            to.FreezeTime();

        }

        if (GUILayout.Button("EDITOR UNFREEZE"))
        {

            to.UnfreezeTime();

        }

        EditorGUILayout.EndHorizontal();

    }

    //SerializedProperty TimePointDelta;

    //void OnEnable()
    //{
    //    TimePointDelta = serializedObject.FindProperty("_timePointDelta");
    //}

    //public override void OnInspectorGUI()
    //{
    //    serializedObject.Update();
    //    EditorGUILayout.LabelField("(Above this object)");
    //    EditorGUILayout.ObjectField();
    //    //if (lookAtPoint.vector3Value.y > (target as LookAtPoint).transform.position.y)
    //    //{
    //    //    EditorGUILayout.LabelField("(Above this object)");
    //    //}
    //    //if (lookAtPoint.vector3Value.y < (target as LookAtPoint).transform.position.y)
    //    //{
    //    //    EditorGUILayout.LabelField("(Below this object)");
    //    //}


    //    serializedObject.ApplyModifiedProperties();
    //}

    //public void OnSceneGUI()
    //{

    //    //var t = (target as LookAtPoint);

    //    //EditorGUI.BeginChangeCheck();
    //    //Vector3 pos = Handles.PositionHandle(t.lookAtPoint, Quaternion.identity);
    //    //if (EditorGUI.EndChangeCheck())
    //    //{
    //    //    Undo.RecordObject(target, "Move point");
    //    //    t.lookAtPoint = pos;
    //    //    t.Update();
    //    //}
    //}

}
