using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(TimeObject))]
public class TimeObjectEditor : Editor
{

    const string ALLRecording = "\n\n *ALL recording means to record even frames where time object doesn't move";

    public override void OnInspectorGUI()
    {

        TimeObject to = (TimeObject)target;

        if (to.CustomEditor)
        {

            CreateCustomGUI(to);

        }
        else
        {

            DrawDefaultInspector();

        }

    }

    public void CreateCustomGUI(TimeObject to)
    {

        base.OnInspectorGUI();

        GUIStyle colorChange = new GUIStyle(GUI.skin.label);
        colorChange.normal.textColor = to.CurrentTimeState == TimeState.RECORDING ? Color.red : Color.green;

        EditorGUILayout.BeginHorizontal();

        GUILayout.Label("● NOT RECORDING", colorChange);

        if (to.CurrentTimeState == TimeState.FROZEN)
        {

            if (GUILayout.Button(new GUIContent("▮▮", "Unfreeze Time Object"), new GUILayoutOption[] { GUILayout.Width(30), GUILayout.Height(20) }))
            {

                to.UnfreezeTime();

            }

        }
        else
        {

            if (GUILayout.Button(new GUIContent("▶", "Freeze Time Object"), new GUILayoutOption[] { GUILayout.Width(30), GUILayout.Height(30) }))
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

        if (GUILayout.Button(new GUIContent("START RECORDING", "Start regular recording")))
        {

            to.StartRecording();

        }

        if (GUILayout.Button(new GUIContent("START RECORDING ALL", $"Start ALL* recording {ALLRecording}")))
        {

            to.StartRecording(false, true);

        }

        if (GUILayout.Button(new GUIContent("CLEAR AND START RECORDING", "Clear recorded time point history and start recording")))
        {

            to.StartRecording();

        }

        if (GUILayout.Button(new GUIContent("CLEAR AND START RECORDING ALL", $"Clear recorded time point history and start ALL* recording {ALLRecording}")))
        {

            to.StartRecording(true, true);

        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button(new GUIContent("END RECORDING", "END RECORDING")))
        {

            to.EndRecording();

        }

        if (GUILayout.Button(new GUIContent("CLEAR RECORDING", "CLEAR RECORDING")))
        {

            to.ClearTimeHistory();

        }

        if (GUILayout.Button(new GUIContent("SAVE RECORDING", "Save recording to a JSON file to be loaded later")))
        {

            TimeObject.SaveSerializedTimeObjectDelta(to);

        }

        if (GUILayout.Button(new GUIContent("LOAD RECORDING", "Load saved recording onto Time Object")))
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
