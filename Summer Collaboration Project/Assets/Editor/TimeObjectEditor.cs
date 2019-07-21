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

        GUIStyle colorChange = new GUIStyle(GUI.skin.label);
        colorChange.normal.textColor = to.CurrentTimeState == TimeState.RECORDING ? Color.red : Color.green;

        EditorGUILayout.BeginHorizontal();

        GUILayout.Label("● NOT RECORDING", colorChange);

        if (to.CurrentTimeState == TimeState.FROZEN)
        {

            if (GUILayout.Button(new GUIContent("▮▮", "Unfreeze"), new GUILayoutOption[] { GUILayout.Width(30), GUILayout.Height(20)}))
            {

                to.UnfreezeTime();
                
            }

        }
        else
        {

            if (GUILayout.Button(new GUIContent("▶", "Freeze"), new GUILayoutOption[] { GUILayout.Width(30), GUILayout.Height(30)}))
            {

                to.FreezeTime();

            }

        }

        if (GUILayout.Button(new GUIContent("◀◀", "Reverse Recording")))
        {

            to.ReverseTime(false);

        }

        if (GUILayout.Button(new GUIContent("▶▶", "Forward Recording")))
        {

            to.ForwardTime();

        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("START RECORDING"))
        {

            to.StartRecording();

        }

        if (GUILayout.Button("START RECORDING ALL"))
        {

            to.StartRecording(false, true);

        }

        if (GUILayout.Button("CLEAR AND START RECORDING"))
        {

            to.StartRecording();

        }

        if (GUILayout.Button("CLEAR AND START RECORDING ALL"))
        {

            to.StartRecording(true, true);

        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

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
