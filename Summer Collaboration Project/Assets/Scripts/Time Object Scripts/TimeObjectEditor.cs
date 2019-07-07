using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TimeObject))]
public class TimeObjectEditor : Editor
{

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
